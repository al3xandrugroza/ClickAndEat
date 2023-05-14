using AutoMapper;
using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.RepositoryServices.ShoppingCartRepository;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    private readonly string _organizationId;
    private readonly string _userId;
    private readonly string _userEmail;

    public ShoppingCartRepository(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;


        _organizationId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("org_id"))?.Value;
        _userId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
        _userEmail = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("email"))?.Value;
    }

    public async Task<ShoppingCartDto?> GetShoppingCart(CancellationToken cancellationToken)
    {
        var shoppingCartEntity = await _context.ShoppingCartEntities.FirstOrDefaultAsync(sc => sc.UserEntityId.Equals(new Guid(_userId)), cancellationToken);
        if (shoppingCartEntity is null) return null;

        await _context.Entry(shoppingCartEntity)
            .Collection(sc => sc.FoodTypeEntities)
            .LoadAsync(cancellationToken);

        var shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCartEntity);
        shoppingCartDto.CartItems = _mapper.Map<IEnumerable<FoodTypeDto>>(shoppingCartEntity.FoodTypeEntities);

        return shoppingCartDto;
    }

    public async Task<ShoppingCartDto> CreateShoppingCart(CancellationToken cancellationToken)
    {
        var shoppingCartEntity = new ShoppingCartEntity
        {
            OrganizationEntityId = new Guid(_organizationId),
            UserEntityId = new Guid(_userId)
        };

        await _context.AddAsync(shoppingCartEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ShoppingCartDto>(shoppingCartEntity);
    }

    public async Task<ShoppingCartDto> UpdateShoppingCart(ShoppingCartDto shoppingCartDto, CancellationToken cancellationToken)
    {
        var organizationMenuEntity = await _context.MenuEntities.SingleOrDefaultAsync(cancellationToken) ?? throw new Exception("not found");
        await _context.Entry(organizationMenuEntity)
            .Collection(sc => sc.FoodTypeEntities)
            .LoadAsync(cancellationToken);

        if (shoppingCartDto.CartItems.Count() > organizationMenuEntity.ShoppingLimit) throw new Exception("bad request");

        var shoppingCartEntity = await _context.ShoppingCartEntities.SingleOrDefaultAsync(sc => sc.UserEntityId.Equals(new Guid(_userId)), cancellationToken) ?? throw new Exception("not found");
        await _context.Entry(shoppingCartEntity)
            .Collection(sc => sc.FoodTypeEntities)
            .LoadAsync(cancellationToken);

        shoppingCartEntity.FoodTypeEntities.Clear();
        foreach (var foodTypeDto in shoppingCartDto.CartItems)
        {
            var foodTypeEntity = await _context.FoodTypeEntities.SingleOrDefaultAsync(ft => ft.Identifier.Equals(foodTypeDto.Identifier) &&
                organizationMenuEntity.FoodTypeEntities.Contains(ft), cancellationToken);
            if (foodTypeEntity is not null) shoppingCartEntity.FoodTypeEntities.Add(foodTypeEntity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var resultShoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCartEntity);
        resultShoppingCartDto.CartItems = _mapper.Map<IEnumerable<FoodTypeDto>>(shoppingCartEntity.FoodTypeEntities);

        return resultShoppingCartDto;
    }

    public async Task ClearShoppingCart(CancellationToken cancellationToken)
    {
        var shoppingCartEntity = await _context.ShoppingCartEntities.FirstOrDefaultAsync(sc => sc.UserEntityId.Equals(new Guid(_userId)), cancellationToken) ?? throw new Exception("not found");
        await _context.Entry(shoppingCartEntity)
            .Collection(sc => sc.FoodTypeEntities)
            .LoadAsync(cancellationToken);

        shoppingCartEntity.FoodTypeEntities.Clear();

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ClearAllShoppingCarts(CancellationToken cancellationToken)
    {
        var shoppingCarts = await _context.ShoppingCartEntities.ToListAsync(cancellationToken);

        foreach (var shoppingCart in shoppingCarts)
        {
            await _context.Entry(shoppingCart)
                .Collection(sc => sc.FoodTypeEntities)
                .LoadAsync(cancellationToken);

            shoppingCart.FoodTypeEntities.Clear();
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
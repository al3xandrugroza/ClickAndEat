using AutoMapper;
using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.RepositoryServices.MenuRepository;

public class MenuRepository : IMenuRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    private readonly string _organizationId;
    // private readonly string _userId;
    // private readonly string _userEmail;

    private const int DefaultShoppingLimit = 3;

    public MenuRepository(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;


        _organizationId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("org_id"))?.Value;
        // _userId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
        // _userEmail = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("email"))?.Value;

    }

    public async Task<MenuDto?> GetMenu(CancellationToken cancellationToken)
    {
        var menuEntity = await _context.MenuEntities.FirstOrDefaultAsync(cancellationToken);
        if (menuEntity is null) return null;

        await _context.Entry(menuEntity)
            .Collection(m => m.FoodTypeEntities)
            .LoadAsync(cancellationToken);

        var menuDto = _mapper.Map<MenuDto>(menuEntity);
        menuDto.ChoiceList = _mapper.Map<IEnumerable<FoodTypeDto>>(menuEntity.FoodTypeEntities);

        return menuDto;
    }

    public async Task<MenuDto> CreateMenu(CancellationToken cancellationToken)
    {
        var menuEntity = new MenuEntity
        {
            ShoppingLimit = DefaultShoppingLimit,
            OrderLockState = OrderLockState.Locked,
            OrganizationEntityId = new Guid(_organizationId)
        };

        await _context.AddAsync(menuEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MenuDto>(menuEntity);
    }

    public async Task<MenuDto> UpdateMenu(MenuUpdateRequestDto menuUpdateRequestDto, CancellationToken cancellationToken)
    {
        var menuEntity = await _context.MenuEntities.FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("not found");
        await _context.Entry(menuEntity)
            .Collection(m => m.FoodTypeEntities)
            .LoadAsync(cancellationToken);

        menuEntity.FoodTypeEntities.Clear();

        foreach (var foodTypeDto in menuUpdateRequestDto.UpdatedChoiceList)
        {
            var foodTypeEntity = await _context.FoodTypeEntities.SingleOrDefaultAsync(ft => ft.Identifier.Equals(foodTypeDto.Identifier), cancellationToken);
            if (foodTypeEntity is not null) menuEntity.FoodTypeEntities.Add(foodTypeEntity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var menuDto = _mapper.Map<MenuDto>(menuEntity);
        menuDto.ChoiceList = _mapper.Map<IEnumerable<FoodTypeDto>>(menuEntity.FoodTypeEntities);

        return menuDto;
    }

    public async Task LockMenu(CancellationToken cancellationToken)
    {
        var menuEntity = await _context.MenuEntities.FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("not found");
        menuEntity.OrderLockState = OrderLockState.Locked;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UnlockMenu(CancellationToken cancellationToken)
    {
        var menuEntity = await _context.MenuEntities.FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("not found");
        menuEntity.OrderLockState = OrderLockState.Unlocked;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
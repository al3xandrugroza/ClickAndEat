using AutoMapper;
using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.RepositoryServices.FoodTypeRepository;

public class FoodTypeRepository : IFoodTypeRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    private readonly string _organizationId;


    public FoodTypeRepository(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;

        _organizationId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("org_id"))?.Value;
    }

    public async Task<IEnumerable<FoodTypeDto>> GetAllFoodTypes(CancellationToken cancellationToken)
    {
        var foodTypeEntities = await _context.FoodTypeEntities.ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<FoodTypeDto>>(foodTypeEntities);
    }

    public async Task<FoodTypeDto> GetFoodTypeByIdentifier(Guid identifier, CancellationToken cancellationToken)
    {
        var foodTypeEntity = await _context.FoodTypeEntities.SingleOrDefaultAsync(ft => ft.Identifier.Equals(identifier), cancellationToken) ?? throw new Exception("not found");
        return _mapper.Map<FoodTypeDto>(foodTypeEntity);
    }

    public async Task<FoodTypeDto> CreateFoodType(FoodTypeCreateRequestDto foodTypeCreateRequestDto, CancellationToken cancellationToken)
    {
        var foodByTypeEntity = await _context.FoodTypeEntities.SingleOrDefaultAsync(ft => ft.Type.Equals(foodTypeCreateRequestDto.Type), cancellationToken);
        if (foodByTypeEntity is not null) throw new Exception("conflict");

        var foodTypeEntity = _mapper.Map<FoodTypeEntity>(foodTypeCreateRequestDto);
        foodTypeEntity.OrganizationEntityId = new Guid(_organizationId);
        _context.FoodTypeEntities.Add(foodTypeEntity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<FoodTypeDto>(foodTypeEntity);
    }

    public async Task<FoodTypeDto> UpdateFoodType(FoodTypeUpdateRequestDto foodTypeUpdateRequestDto, CancellationToken cancellationToken)
    {
        var foodTypeEntity = await _context.FoodTypeEntities.FirstOrDefaultAsync(ft => ft.Identifier.Equals(foodTypeUpdateRequestDto.Identifier), cancellationToken) ?? throw new Exception("not found");
        
        foodTypeEntity.Description = foodTypeUpdateRequestDto.Description;
        foodTypeEntity.Type = foodTypeUpdateRequestDto.Type;
        foodTypeEntity.ImageLink = foodTypeUpdateRequestDto.ImageLink;
        
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<FoodTypeDto>(foodTypeEntity);
    }

    public async Task DeleteFoodType(Guid identifier, CancellationToken cancellationToken)
    {
        var foodTypeEntity = await _context.FoodTypeEntities.FirstOrDefaultAsync(ft => ft.Identifier.Equals(identifier), cancellationToken) ?? throw new Exception("not found");
        
        _context.FoodTypeEntities.Remove(foodTypeEntity);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}
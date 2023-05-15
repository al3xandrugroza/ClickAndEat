using AutoMapper;
using ClickAndEatApi.Db.Models;
using ClickAndEatApi.Dtos;
using ClickAndEatApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace ClickAndEatApi.Db.RepositoryServices.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    private readonly string _organizationId;
    private readonly string _userId;
    private readonly string _userEmail;

    public OrderRepository(AppDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;


        _organizationId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("org_id"))?.Value;
        _userId = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("nameidentifier"))?.Value;
        _userEmail = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(c => c.Type.Contains("email"))?.Value;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrders(CancellationToken cancellationToken)
    {
        var orderEntities = await _context.OrderEntities.ToListAsync(cancellationToken);

        var orderDtos = new List<OrderDto>();
        foreach (var orderEntity in orderEntities)
        {
            await _context.Entry(orderEntity)
                .Collection(o => o.FoodTypeEntities)
                .LoadAsync(cancellationToken);
            await _context.Entry(orderEntity)
                .Reference(o => o.UserEntity)
                .LoadAsync(cancellationToken);

            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            orderDto.OrderedItems = _mapper.Map<IEnumerable<FoodTypeDto>>(orderEntity.FoodTypeEntities);
            orderDto.UserEmail = orderEntity.UserEntity.Email;

            orderDtos.Add(orderDto);
        }

        return orderDtos;
    }

    public async Task<OrderDto?> GetOrder(CancellationToken cancellationToken)
    {
        var orderEntity = await _context.OrderEntities.FirstOrDefaultAsync(o => o.UserEntityId.Equals(new Guid(_userId)), cancellationToken);
        if (orderEntity is null) return null;

        await _context.Entry(orderEntity)
            .Collection(o => o.FoodTypeEntities)
            .LoadAsync(cancellationToken);
        await _context.Entry(orderEntity)
            .Reference(o => o.UserEntity)
            .LoadAsync(cancellationToken);

        var orderDto = _mapper.Map<OrderDto>(orderEntity);
        orderDto.OrderedItems = _mapper.Map<IEnumerable<FoodTypeDto>>(orderEntity.FoodTypeEntities);
        orderDto.UserEmail = orderEntity.UserEntity.Email;

        return orderDto;
    }

    public async Task<OrderDto> PlaceOrder(CancellationToken cancellationToken)
    {
        var orderEntity = await _context.OrderEntities.FirstOrDefaultAsync(sc => sc.UserEntityId.Equals(new Guid(_userId)), cancellationToken) ?? await CreateOrderEntity(cancellationToken);

        await _context.Entry(orderEntity)
            .Collection(o => o.FoodTypeEntities)
            .LoadAsync(cancellationToken);
        await _context.Entry(orderEntity)
            .Reference(o => o.UserEntity)
            .LoadAsync(cancellationToken);

        orderEntity.FoodTypeEntities.Clear();
        var shoppingCartEntity = await _context.ShoppingCartEntities.SingleOrDefaultAsync(sc => sc.UserEntityId.Equals(new Guid(_userId)), cancellationToken) ?? throw new Exception("not found");
        await _context.Entry(shoppingCartEntity)
            .Collection(sc => sc.FoodTypeEntities)
            .LoadAsync(cancellationToken);

        foreach (var foodTypeEntity in shoppingCartEntity.FoodTypeEntities)
        {
            orderEntity.FoodTypeEntities.Add(foodTypeEntity);
        }

        await _context.SaveChangesAsync(cancellationToken);

        var orderDto = _mapper.Map<OrderDto>(orderEntity);
        orderDto.OrderedItems = _mapper.Map<IEnumerable<FoodTypeDto>>(orderEntity.FoodTypeEntities);
        orderDto.UserEmail = orderEntity.UserEntity.Email;

        return orderDto;
    }

    private async Task<OrderEntity> CreateOrderEntity(CancellationToken cancellationToken)
    {
        var orderEntity = new OrderEntity
        {
            OrderDeliverState = OrderDeliverState.Pending,
            OrganizationEntityId = new Guid(_organizationId),
            UserEntityId = new Guid(_userId)
        };

        await _context.AddAsync(orderEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return orderEntity;
    }

    public async Task CancelOrder(CancellationToken cancellationToken)
    {
        var orderEntity = await _context.OrderEntities.FirstOrDefaultAsync(sc => sc.UserEntityId.Equals(new Guid(_userId)), cancellationToken);
        if (orderEntity is null) return;

        _context.OrderEntities.Remove(orderEntity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAllOrders(CancellationToken cancellationToken)
    {
        var orderEntities = await _context.OrderEntities.ToListAsync(cancellationToken);
        _context.OrderEntities.RemoveRange(orderEntities);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
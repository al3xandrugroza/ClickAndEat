namespace ClickAndEatApi.Db.Models;

public abstract  class BaseEntity
{
    public Guid Identifier { get; set; }
    
    public Guid OrganizationEntityId { get; set; }
    public OrganizationEntity OrganizationEntity { get; set; }
}
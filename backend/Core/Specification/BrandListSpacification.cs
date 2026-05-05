using Core.Entities;

namespace Core.Specification;

public class BrandListSpacification : BaseSpecification<Product , string>
{

    public BrandListSpacification()
    {
        AddSelect(x => x.Brand);
        ApplayDistinct();   
    }
}

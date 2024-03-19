using Riok.Mapperly.Abstractions;
using Shared.Features;
using Shared.Features.Product;

namespace Service.Features
{
    [Mapper]
    public static partial class ProductMapper
    {
        #region Usable
        public static ProductView MapToView(this ProductEntity src) => src.To();
        public static List<ProductView> MapToViewList(this List<ProductEntity> src) => src.ToList();
        public static ProductEntity MapFromView(this ProductView src) => src.From();
        #endregion

        #region Internal


        private static partial ProductView To(this ProductEntity src);

        private static partial List<ProductView> ToList(this List<ProductEntity> src);

        private static partial ProductEntity From(this ProductView ProductView);

        public static partial void From(ProductView personView, ProductEntity personEntity);
        #endregion
    }
}

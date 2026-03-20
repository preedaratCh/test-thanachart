import { Product } from "@/types/product";
import { ProductService } from "@/services/product.service";
import PageHeader from "@/components/PageHeader";
import ProductGrid from "@/components/product/ProductGrid";

export default async function ProductPage() {
    let products: Product[] = [];
    const productService = new ProductService();

    try {
        products = await productService.getProducts();
    } catch {
        return <p className="p-6 text-red-600">Failed to load products.</p>;
    }

    return (
        <div className="">
            <PageHeader
                title="Products"
                subtitle="Browse all available products"
            />
            {products.length === 0 ? (
                <p className="p-6 text-gray-600">No products available.</p>
            ) : (
                <ProductGrid products={products} />
            )}
        </div>
    );
}
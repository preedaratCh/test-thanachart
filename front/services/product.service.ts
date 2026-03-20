import { Product } from "@/types/product";

export class ProductService {
    private readonly baseUrl: string;

    constructor(apiBaseUrl: string = 'http://localhost:5000/api/products') {
        this.baseUrl = apiBaseUrl;
    }
    async getProducts(): Promise<Product[]> {
        try {
            const response = await fetch(this.baseUrl, {
                headers: {
                    'Accept': 'application/json',
                },
                cache: 'no-store'
            });

            if (!response.ok) {
                throw new Error(`Failed to fetch products: ${response.status} ${response.statusText}`);
            }

            const products = await response.json();

            return products;
        } catch (error: unknown) {
            throw new Error(`An error occurred while fetching products: ${error instanceof Error ? error.message : String(error)}`);
        }
    }
}


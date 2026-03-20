type InventoryDto = {
  id: string;
  productId: string;
  quantity: number;
};

export class InventoryService {
  private readonly baseUrl: string;

  constructor(apiBaseUrl: string = "http://localhost:5000/api/inventories") {
    this.baseUrl = apiBaseUrl;
  }

  async getInventories(): Promise<InventoryDto[]> {
    try {
      const response = await fetch(this.baseUrl, {
        headers: {
          Accept: "application/json",
        },
        cache: 'no-store'
      });

      if (!response.ok) {
        throw new Error(
          `Failed to fetch inventories: ${response.status} ${response.statusText}`
        );
      }

      const inventories = (await response.json()) as InventoryDto[];
      return inventories;
    } catch (error: unknown) {
      throw new Error(
        `An error occurred while fetching inventories: ${
          error instanceof Error ? error.message : String(error)
        }`
      );
    }
  }

  async getAvailableQuantity(productId: string): Promise<number | null> {
    const inventories = await this.getInventories();
    const inventory = inventories.find((item) => item.productId === productId);
    return inventory?.quantity ?? null;
  }
}

using SimpleInventoryAPI.Data;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Seeder
{
    public class Seeder
    {
        private readonly SimpleInventoryDbContext _context;

        public Seeder( SimpleInventoryDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (isDbEmpty())
            {
                IEnumerable<InventoryMove> inventoryMoves = new List<InventoryMove>()
                {
                    new InventoryMove()
                    {
                        People = new People()
                        {
                            Name = "Joe Doe",
                            Document = "29430sfds"
                        },
                        Items = new List<InventoryMoveItem>()
                        {
                            new InventoryMoveItem()
                            {
                                Product = new Product()
                                {
                                    Name = "Pen",
                                    Cost = (decimal).30
                                },
                                Quantity = 2,
                                UnitPrice = (decimal).60
                            }
                        },
                        MoveType = Data.InventoryMoveType.Sale
                    },
                    new InventoryMove()
                    {
                         People = new People()
                        {
                            Name = "Joana Doe",
                            Document = "af21afv"
                        },
                        Items = new List<InventoryMoveItem>()
                        {
                            new InventoryMoveItem()
                            {
                                Product = new Product()
                                {
                                    Name = "Pencil",
                                    Cost = (decimal).30
                                },
                                Quantity = 20,
                                UnitPrice = (decimal).20
                            },
                            new InventoryMoveItem()
                            {
                                Product = new Product()
                                {
                                    Name = "Papper",
                                    Cost = (decimal).02
                                },
                                Quantity = 200,
                                UnitPrice = (decimal).02
                            }
                        },
                        MoveType = Data.InventoryMoveType.Purchase
                    }
                };

                _context.AttachRange(inventoryMoves);
                _context.SaveChanges();
            }
        }

        private bool isDbEmpty()
        {
            return (!_context.inventoryMoves.Any() && 
                    !_context.Peoples.Any() && 
                    !_context.Products.Any() && 
                    !_context.inventoryMoveItems.Any());
        }

       
    }
}

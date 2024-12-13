open System
open System.Drawing
open System.Windows.Forms

// Define the Product record type
type Product = 
    { Name: string
      Price: float
      Description: string
      ImagePath: string } // Path to the product image

// Helper function to resolve the full image path
let getImagePath relativePath = 
    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath)

// Initialize the product catalog
let productCatalog = 
    [ { Name = "Laptop"; Price = 999.99;     Description = "High-performance laptop with 16GB RAM and 512GB SSD."; ImagePath = getImagePath "assets/laptop.jpg" }
      { Name = "Smartphone"; Price = 699.99; Description = "Latest smartphone with a stunning display and powerful camera."; ImagePath = getImagePath "assets/smartphone.jpg" }
      { Name = "Headphones"; Price = 199.99; Description = "Noise-cancelling over-ear headphones with rich sound quality."; ImagePath = getImagePath "assets/headphones.jpg" }
      { Name = "Smartwatch"; Price = 249.99; Description = "Feature-packed smartwatch with health and fitness tracking."; ImagePath = getImagePath "assets/smartwatch.jpg" }
      { Name = "Bag"; Price = 59.99;        Description = "Durable backpack with multiple compartments for daily use."; ImagePath = getImagePath "assets/page.jpg" }
      { Name = "Shoes"; Price = 89.99;       Description = "Comfortable running shoes with a durable sole."; ImagePath = getImagePath "assets/shoes.jpg" } ]

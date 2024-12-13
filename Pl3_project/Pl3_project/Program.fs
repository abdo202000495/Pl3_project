open System


type Product = 
    { Name: string
      Price: float
      Description: string
      ImagePath: string }


let getImagePath relativePath = 
    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath)


let productCatalog = 
    [ { Name = "Laptop"; Price = 999.99; Description = "High-end gaming laptop"; ImagePath = "images/laptop.jpg" }
      { Name = "Phone"; Price = 699.99; Description = "Latest smartphone"; ImagePath = "images/phone.jpg" }
      { Name = "Headphones"; Price = 199.99; Description = "Noise-cancelling headphones"; ImagePath = "images/headphones.jpg" } ]


productCatalog 
|> List.iter (fun product -> 
    printfn "Product: %s, Price: $%.2f, Description: %s, Image Path: %s" 
        product.Name product.Price product.Description (getImagePath product.ImagePath))

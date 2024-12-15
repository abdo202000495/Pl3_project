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
// Part 2: Cart Form
let createCartForm () =
    let form = new Form(Text = "Shopping Cart", Width = 600, Height = 500, BackColor = Color.White)
    let cartPanel = new FlowLayoutPanel(Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.Beige)
    let totalLabel = new Label(Text = "Total: $0.00", Dock = DockStyle.Bottom, Font = new Font("Arial", 12.0f, FontStyle.Bold), Height = 40, ForeColor = Color.DarkRed, TextAlign = ContentAlignment.MiddleCenter)
    let checkoutButton = new Button(Text = "Checkout", Dock = DockStyle.Bottom, Height = 40, BackColor = Color.Green, ForeColor = Color.White)

    // Populate the cart with items
    cart |> List.iter (fun product ->
        let cartItemPanel = new Panel(Width = 550, Height = 100, Margin = Padding(10), BackColor = Color.WhiteSmoke, BorderStyle = BorderStyle.FixedSingle)
        let nameLabel = new Label(Text = product.Name, Dock = DockStyle.Left, Width = 200, Font = new Font("Arial", 10.0f), ForeColor = Color.Black, TextAlign = ContentAlignment.MiddleLeft)
        let priceLabel = new Label(Text = sprintf "$%.2f" product.Price, Dock = DockStyle.Right, Width = 100, Font = new Font("Arial", 10.0f), ForeColor = Color.DarkGreen, TextAlign = ContentAlignment.MiddleRight)

        // Add controls to the cart item panel
        cartItemPanel.Controls.Add(nameLabel)
        cartItemPanel.Controls.Add(priceLabel)

        // Add the cart item panel to the cart panel
        cartPanel.Controls.Add(cartItemPanel)
    )

    // Calculate total price
    let totalPrice = cart |> List.sumBy (fun product -> product.Price)
    totalLabel.Text <- sprintf "Total: $%.2f" totalPrice

    // Checkout button functionality
    checkoutButton.Click.Add(fun _ ->
        MessageBox.Show("Checkout complete!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
        cart <- [] // Clear the cart after checkout
        form.Close()
    )

    // Add components to the cart form
    form.Controls.Add(cartPanel)
    form.Controls.Add(totalLabel)
    form.Controls.Add(checkoutButton)
    form

// Main Entry Point
[<EntryPoint>]
let main argv =
    let mainForm = createMainForm()
    Application.Run(mainForm)
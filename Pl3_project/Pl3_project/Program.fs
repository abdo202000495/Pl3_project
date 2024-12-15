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

// The user's shopping cart
let mutable cart: Product list = []

// Function to create the cart form
let createCartForm () =
    let form = new Form(Text = "Cart", Width = 800, Height = 600, BackColor = Color.White)
    let cartPanel = new FlowLayoutPanel(Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.LightGray)
    let totalLabel = new Label(Text = "Total: $0.00", Width = 150, Height = 30, Dock = DockStyle.Bottom, Font = new Font("Arial", 12.0f, FontStyle.Bold))
    let checkoutButton = new Button(Text = "Checkout", Width = 150, Height = 40, Dock = DockStyle.Bottom, BackColor = Color.LightGreen)

    // Function to update and display the cart
    let rec viewCart () =
        cartPanel.Controls.Clear()
        if cart.IsEmpty then
            let emptyLabel = new Label(Text = "Your cart is empty.", Font = new Font("Arial", 12.0f, FontStyle.Bold), Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter)
            cartPanel.Controls.Add(emptyLabel)
        else
            cart |> List.iter (fun product ->
                let productPanel = new Panel(Width = 200, Height = 300, Margin = Padding(10), BackColor = Color.WhiteSmoke)
                let pictureBox = new PictureBox(Width = 180, Height = 150, SizeMode = PictureBoxSizeMode.StretchImage, Dock = DockStyle.Top, Margin = Padding(10))
                let nameLabel = new Label(Text = product.Name, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Top, Font = new Font("Arial", 10.0f, FontStyle.Bold))
                let priceLabel = new Label(Text = sprintf "$%.2f" product.Price, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Top, Font = new Font("Arial", 10.0f))
                let removeButton = new Button(Text = "Remove", Dock = DockStyle.Bottom, Height = 30, BackColor = Color.LightCoral, ForeColor = Color.White, Font = new Font("Arial", 9.0f, FontStyle.Bold))

                // Load the product image
                try
                    pictureBox.Image <- Image.FromFile(product.ImagePath)
                with
                    | :? System.IO.FileNotFoundException -> 
                        pictureBox.Image <- null
                        nameLabel.Text <- "Image not found!\n" + nameLabel.Text

                // Remove product from cart on button click
                removeButton.Click.Add(fun _ ->
                    cart <- cart |> List.filter (fun p -> p <> product)
                    viewCart() // Refresh the cart view and total
                )

                // Add controls to the product panel
                productPanel.Controls.Add(removeButton)
                productPanel.Controls.Add(pictureBox)
                productPanel.Controls.Add(nameLabel)
                productPanel.Controls.Add(priceLabel)
                
                // Add the product panel to the cart panel
                cartPanel.Controls.Add(productPanel)
            )
            // Update the total cost after the cart is modified
            let totalCost = cart |> List.sumBy (fun p -> p.Price)
            totalLabel.Text <- sprintf "Total: $%.2f" totalCost

    // Checkout functionality
    checkoutButton.Click.Add(fun _ ->
        MessageBox.Show(sprintf "Thank you for shopping! Total: $%.2f" (cart |> List.sumBy (fun p -> p.Price)), "Checkout Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        cart <- []
        viewCart()
    )

    // Add controls to form
    form.Controls.Add(cartPanel)
    form.Controls.Add(totalLabel)
    form.Controls.Add(checkoutButton)
    viewCart()
    form

// Function to create the main form
let createMainForm () =
    let form = new Form(Text = "Store System", Width = 800, Height = 700, BackColor = Color.White)
    let flowPanel = new FlowLayoutPanel(Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.LightGray)
    let viewCartButton = new Button(Text = "View Cart", Width = 150, Height = 40, Dock = DockStyle.Bottom, BackColor = Color.LightBlue)

    // Create a panel for each product
    productCatalog |> List.iter (fun product ->
        let productPanel = new Panel(Width = 200, Height = 300, Margin = Padding(10), BackColor = Color.WhiteSmoke)
        let pictureBox = new PictureBox(Width = 180, Height = 150, SizeMode = PictureBoxSizeMode.StretchImage, Dock = DockStyle.Top, Margin = Padding(10))
        let nameLabel = new Label(Text = product.Name, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Top, Font = new Font("Arial", 10.0f, FontStyle.Bold))
        let priceLabel = new Label(Text = sprintf "$%.2f" product.Price, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Top, Font = new Font("Arial", 10.0f))
        let descriptionLabel = new Label(Text = product.Description, TextAlign = ContentAlignment.TopCenter, Dock = DockStyle.Fill, Font = new Font("Arial", 9.0f), Padding = Padding(5))
        let addButton = new Button(Text = "Add to Cart", Dock = DockStyle.Bottom, Height = 30, BackColor = Color.LightGreen, ForeColor = Color.DarkBlue, Font = new Font("Arial", 9.0f, FontStyle.Bold))

        // Load the product image
        try
            pictureBox.Image <- Image.FromFile(product.ImagePath)
        with
            | :? System.IO.FileNotFoundException -> 
                pictureBox.Image <- null
                descriptionLabel.Text <- "Image not found!\n" + descriptionLabel.Text

        // Add product to cart on button click
        addButton.Click.Add(fun _ -> 
            cart <- cart @ [product]
            MessageBox.Show(sprintf "%s added to the cart." product.Name, "Added to Cart", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
        )

        // Add controls to the product panel
        productPanel.Controls.Add(addButton)
        productPanel.Controls.Add(descriptionLabel)
        productPanel.Controls.Add(priceLabel)
        productPanel.Controls.Add(nameLabel)
        productPanel.Controls.Add(pictureBox)
        
        // Add the product panel to the flow panel
        flowPanel.Controls.Add(productPanel)
    )

    // View Cart button functionality
    viewCartButton.Click.Add(fun _ ->
        let cartForm = createCartForm()
        cartForm.Show()
    )

    // Add the flow panel and view cart button to the main form
    form.Controls.Add(flowPanel)
    form.Controls.Add(viewCartButton)
    form

// Run the application
[<EntryPoint>]
let main argv =
    let mainForm = createMainForm()
    Application.Run(mainForm)
    0

﻿
open System
open System.Collections.Generic

type Product = {
    Name: string
    Price: float
    ImagePath: string
    Category: string
}

let mutable cart: Product list = []

let addToCart (product: Product) =
    cart <- product :: cart

let removeFromCart (product: Product) =
    cart <- cart |> List.filter (fun p -> p <> product)

let calculateTotal () =
    cart |> List.sumBy (fun p -> p.Price)

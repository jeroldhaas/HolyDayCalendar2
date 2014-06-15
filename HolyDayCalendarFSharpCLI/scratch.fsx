#i "System.Text.dll"

open System
open System.Text

// TODO: compare StringBuilder object to concatenate operator
let concat = "one " + "two " + "three"

let buildstring =
    let mutable sb = new StringBuilder()
    sb <- sb.Append("one ")
    sb <- sb.Append("two ")
    sb <- sb.Append("three ")
    sb.ToString()
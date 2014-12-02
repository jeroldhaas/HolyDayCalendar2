module Months

open Days


type SolarMonths =
| January = 0
| February = 1
| March = 2
| April = 3
| May = 4
| June = 5
| July = 6
| August = 7
| September = 8
| October = 9
| November = 10
| December = 11


type LunarMonths =
| Tishri = 0
| Heshvan = 1
| Kislev = 2
| Tebeth = 3
| Shebat = 4
| ``Adar I`` = 5
| ``Adar II`` = 6
| Nisan = 7
| Iyar = 8
| Sivan = 9
| Tammuz = 10
| Ab = 11
| Elul = 12
| Adar = 13


type Month =
    member val Num = 0   with get, set
    member val Name = "" with get, set
    member val NDays = 0 with get, set
    member val Days :Day[] = Array.empty with get, set
    member val Year = new Year() with get, set

    //member val x.Prev =

    new (num :int, name :string) as x =
        x.Num   <- num
        x.Name  <- name
        x.NDays <- 0
        x.Days  <- Array.empty 
        x.Year  <- None
        x

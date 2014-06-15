namespace HolyDayCalendar

open System
open NodaTime
open System.Collections
open System.Collections.Generic
open Microsoft.FSharp.Collections


(*
    Passover, (2nd Passover?), Unleavened Bread, Pentecost, Feast of Trumpets,
    Day of Atonement, Feast of Tabernacles, Last Great Day
*)
type HolyDay = {
    Name : string;
    // Year-Month-Day-Weekday used for calculating holy day from Coptic calendar
    Year : int option;
    Month : int option;
    Day : int option;
    Weekday : int option;
}

type Constants =
    (*
        TODO: fill this in, if possible, with HolyDay
        records with the proper constant values based on
        Bible laws & statutes
    *)
    member this.Days = [
                        { Name = "Pentecost";
                          Year = None;
                          Month = Some 3;
                          Day = Some 50;
                          Weekday = None;
                        };
                        { Name = "Unleavened Bread";
                          Year = None;
                          Month = Some 3;
                          Day = Some 4;
                          Weekday = None;
                        };
                        { Name = "Feast of Trumpets";
                          Year = None;
                          Month = Some 2;
                          Day = Some 2
                          Weekday = None;
                        };
                        { Name = "2nd Passover";
                          Year = None;
                          Month = Some 3;
                          Day = Some 3;
                          Weekday = None;
                        };
                        { Name = "Feast of Trumpets";
                          Year = None;
                          Month = Some 5;
                          Day = Some 5;
                          Weekday = None;
                        };
                        { Name = "Day of Atonement";
                          Year = None;
                          Month = Some 5;
                          Day = Some 5;
                          Weekday = None;
                        };
                        { Name = "Feast of Tabernacles";
                          Year = None;
                          Month = Some 5;
                          Day = Some 5;
                          Weekday = None;
                        };
                        { Name = "Last Great Day";
                          Year = None;
                          Month = Some 5;
                          Day = Some 5;
                          Weekday = None;
                        };                            
                       ]
    member this.invert x y =
        match (x, y) with
            | _,_ when (x > 0 && y > 0) || (x < 0 && y < 0) -> (-x, -y)
            | _,_ when x < 0 || x > 0 -> (-x, y)
            | _,_ when y < 0 || y > 0 -> (x, -y)
            | _,_ -> (x, y)
    member this.timeFunction f =
        let start = DateTime.Now
        let result = f()
        let stop = DateTime.Now
        let elapsed = stop - start
        (result, elapsed.TotalMilliseconds)

type FixedString(len:Int32, value: String) = struct
    end

(*
 * Experimentals
 *)
// structs!
/// <summary>
/// Example of how to use a complex struct type (Date, Time, OHLCV)
/// </summary>
type StructThing(d: DateTime, o: float, h: float, l: float, c: float, v: int) =
    struct
        member this.Date = d.Date
        member this.Time = d.TimeOfDay
        member this.Open = o
        member this.High = h
        member this.Low = l
        member this.Close = c
        member this.Volume = v
    end
type MPoint =
    struct
        val mutable X : int
        val mutable Y : int
        member this.toString() =
            sprintf "{%d, %d}" this.X this.Y
    end

// TODO: Find the conditional syntactic sugar for these to overload
// These are List<'T> in .NET - remember this taste of awesome!
(*
type AwesomeSauce() =
    let SeriouslyCheckIt(len:int) : ResizeArray<string> =
        new ResizeArray<string>(len)
    let SeriouslyCheckIt(coll) when coll: IEnumerable<_> : ResizeArray<_> =
        new ResizeArray<_>(coll)
    member this.SparseMap<'T>(coll: IDictionary<(int * int), 'T>) =
        new Dictionary<(int * int), 'T>(coll)
    member this.SparseCube<'T>(coll: IDictionary<(int * int * int), 'T>) =
        new Dictionary<(int * int * int), 'T>(coll)
*)
// using interfaces (for CLI-OOP compat)!
type IStringer = interface
        abstract member Stringify: string -> string
        abstract member Stringify: byte -> string
        abstract member Stringify: int -> string
    end
type Stringer(v) =
    let mutable value = String.Empty
    do
        value <- v
    member x.String
            with get(): string = value
            and set(v) = value <- v
    interface IStringer with
        member x.Stringify(arg1: string): string = 
            System.Convert.ToString(arg1)
        member x.Stringify(arg1: byte): string = 
            System.Convert.ToString(arg1)
        member x.Stringify(arg1: int): string = 
            System.Convert.ToString(arg1)
    end

//        member Stringer(v: string) =
//            str <- v
            
        


type private OtherClass(n:string, i:int) =
    // private members
    // using default value for empty mutable value
    //[<DefaultValue>]
    //private mutable val thisValue : string
    let mutable name = n
    let mutable num = i
    // public members (get/set)
    member this.Name
        with get() = name
        and set(v) = name <- v
    member this.Num
        with get() = num
        and set(v) = num <- v
    // constructors???
    new(nom:string) =
        new OtherClass(nom, 0)
    new(i:int) =
        new OtherClass("", i)
    // static members
    static member FancyStatic(a:int, b:int, c:int) =
        a + b + c


type ConstructorOverrideClass(x:int, y:int, z:int, name: string) =
    member this.X = x
    member this.Y = y
    member this.Z = z
    member this.Name = name
    new() = new ConstructorOverrideClass(0, 0, 0, "Generic")
    new(name:string) = new ConstructorOverrideClass(0, 0, 0, name)

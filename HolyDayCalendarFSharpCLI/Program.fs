// Documentation for Libs (installed with Nuget and others)
// - http://nodatime.org/

open System
open System.Globalization
open System.Xml
open NodaTime



[<EntryPoint>]
let main argv = 
    let nowJulian = new LocalDateTime(
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        DateTime.Now.Hour,
                        DateTime.Now.Minute,
                        CalendarSystem.GetJulianCalendar(0) // TODO: this int may need adjustment for accuracy
    )
    let nowCoptic = nowJulian.WithCalendar(CalendarSystem.GetCopticCalendar(0)) // TODO: adjust int?

    printfn "
        Julian: %A
        Coptic: %A"
        (nowJulian.ToString(), nowCoptic.ToString()) |> ignore // was using argv
    0 // return an integer exit code
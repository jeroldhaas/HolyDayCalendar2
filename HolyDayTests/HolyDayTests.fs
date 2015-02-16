module HolyDayTests

open System
open System.Xml

open FsUnit
open FsCheck
open NodaTime

//let ``check conversion from one calendar to the other`` =
let now = System.DateTime.Now
let nodaNow = new NodaTime.LocalDate(now.Year, now.Month, now.Day)

let civilNumbering = Calendars.HebrewMonthNumbering.Civil
let bibNumbering = Calendars.HebrewMonthNumbering.Scriptural

let hebCivilCal = CalendarSystem.GetHebrewCalendar(civilNumbering)
let hebScriptCal = CalendarSystem.GetHebrewCalendar(bibNumbering)

let hebrewNodaNow = nodaNow.WithCalendar(hebCivilCal)
let scripturalNodaNow = nodaNow.WithCalendar(hebScriptCal)



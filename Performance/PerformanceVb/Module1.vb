﻿Imports System
Imports System.Diagnostics
Imports GemBox.Spreadsheet

Module Module1

    Sub Main()

        ' If using Professional version, put your serial key below.
        SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY")

        ' If sample exceeds Free version limitations then continue as trial version:
        ' https://www.gemboxsoftware.com/spreadsheet/help/html/Evaluation_and_Licensing.htm
        AddHandler SpreadsheetInfo.FreeLimitReached, Sub(sender, e) e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial

        Dim rowCount As Integer = 100000
        Dim columnCount As Integer = 10
        Dim fileFormat As String = "XLSX"

        Console.WriteLine("Performance sample:")
        Console.WriteLine()
        Console.WriteLine("Row count: " & rowCount)
        Console.WriteLine("Column count: " & columnCount)
        Console.WriteLine("File format: " & fileFormat)
        Console.WriteLine()

        Dim stopwatch As New Stopwatch()
        stopwatch.Start()

        Dim ef As New ExcelFile()
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("Performance")

        For row As Integer = 0 To rowCount - 1
            For column As Integer = 0 To columnCount - 1
                ws.Cells(row, column).Value = row.ToString() & "_" & column
            Next
        Next

        Console.WriteLine("Generate file (seconds): " & stopwatch.Elapsed.TotalSeconds)

        stopwatch.Reset()
        stopwatch.Start()

        Dim cellsCount As Integer = 0
        For Each row As ExcelRow In ws.Rows
            For Each cell As ExcelCell In row.AllocatedCells
                cellsCount += 1
            Next
        Next

        Console.WriteLine("Iterate through " & cellsCount & " cells (seconds): " & stopwatch.Elapsed.TotalSeconds)

        stopwatch.Reset()
        stopwatch.Start()

        ef.Save("Report." & fileFormat.ToLower())

        Console.WriteLine("Save file (seconds): " & stopwatch.Elapsed.TotalSeconds)

    End Sub

End Module

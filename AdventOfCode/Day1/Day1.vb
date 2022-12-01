Imports System.IO

Public Class Day1
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day1\input.txt")
        Dim runningTotal As Integer
        Dim elves As List(Of Integer) = New List(Of Integer)
        runningTotal = 0
        For Each line As String In lines
            If Len(line) < 1 Then
                elves.Add(runningTotal)
                runningTotal = 0
            Else
                runningTotal += Convert.ToInt32(line)
            End If
        Next
        Console.WriteLine(elves.Max)
        elves.Sort()
        Console.WriteLine(elves.GetRange(elves.Count - 3, 3).Sum)
    End Sub
End Class

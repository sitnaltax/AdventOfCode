Public Class Day15
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day15\input.txt")

        'For every sensor
        'calculate the distance to the y = 200000 mark
        'every unit of Manhattan distance left is a place where the beacon cannot be
        'Some may overlap

        'initialize the grid

        Const TargetRow As Integer = 2000000

        Dim ranges As List(Of Range) = New List(Of Range)
        For Each line As String In lines
            Dim reading As New SensorReading(line)
            Dim range As Range = reading.GetRangeInTargetRow(TargetRow)
            If range IsNot Nothing Then
                ranges.Add(range)
            End If
        Next

        'Merge all the ranges
        Dim mergedRanges As List(Of Range) = MergeRanges(ranges)
        Dim runningTotal = 0
        For Each range As Range In mergedRanges
            runningTotal += range.Last - range.First + 1
        Next
        'remove sensors in this row, if there are any

        Console.WriteLine(runningTotal)
    End Sub

    Public Shared Sub calculate2()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day15\input.txt")


        Dim readings As List(Of SensorReading) = New List(Of SensorReading)
        For Each line As String In lines
            readings.Add(New SensorReading(line))
        Next

        For i As Integer = 0 To 4000000
            Dim ranges As List(Of Range) = New List(Of Range)
            For Each reading As SensorReading In readings
                Dim range As Range = reading.GetRangeInTargetRow(i)
                If range IsNot Nothing Then
                    ranges.Add(range)
                End If
            Next
            Dim mergedRanges As List(Of Range) = MergeRanges(ranges)
            If mergedRanges.Count > 1 Then
                Console.WriteLine(i)
                For Each range As Range In mergedRanges
                    Console.WriteLine(range)
                Next
            End If
        Next
    End Sub

    Public Shared Function MergeRanges(ranges As List(Of Range)) As List(Of Range)
        Dim newRangeList As List(Of Range) = New List(Of Range)

        While ranges.Count > 0
            Dim currentRange = ranges(0)
            ranges.RemoveAt(0)
            For i As Integer = ranges.Count - 1 To 0 Step -1
                If currentRange.First <= ranges(i).Last And currentRange.Last >= ranges(i).First Then
                    currentRange = New Range(Math.Min(currentRange.First, ranges(i).First), Math.Max(currentRange.Last, ranges(i).Last))
                    ranges.RemoveAt(i)
                End If
            Next
            newRangeList.Add(currentRange)
        End While

        Dim initialCount = 0

        Do
            initialCount = newRangeList.Count
            Dim examiningRanges As List(Of Range) = newRangeList
            newRangeList = New List(Of Range)
            While examiningRanges.Count > 0
                Dim currentRange = examiningRanges(0)
                examiningRanges.RemoveAt(0)
                For i As Integer = examiningRanges.Count - 1 To 0 Step -1
                    If currentRange.First <= examiningRanges(i).Last And currentRange.Last >= examiningRanges(i).First Then
                        currentRange = New Range(Math.Min(currentRange.First, examiningRanges(i).First), Math.Max(currentRange.Last, examiningRanges(i).Last))
                        examiningRanges.RemoveAt(i)
                    End If
                Next
                newRangeList.Add(currentRange)
            End While
        Loop While initialCount > newRangeList.Count

        Return newRangeList
    End Function

    Public Class SensorReading
        Public SensorX As Integer
        Public SensorY As Integer
        Public BeaconX As Integer
        Public BeaconY As Integer
        Public Sub New(input As String)
            Dim tokens() = input.Split(" ")
            SensorX = Convert.ToInt32(tokens(2).Replace("x=", "").Replace(",", ""))
            SensorY = Convert.ToInt32(tokens(3).Replace("y=", "").Replace(":", ""))
            BeaconX = Convert.ToInt32(tokens(8).Replace("x=", "").Replace(",", ""))
            BeaconY = Convert.ToInt32(tokens(9).Replace("y=", "").Replace(":", ""))
        End Sub

        Public Function GetRangeInTargetRow(targetrow As Integer) As Range
            Dim manhattanDistance As Integer = Math.Abs(BeaconX - SensorX) + Math.Abs(BeaconY - SensorY)
            Dim distanceToTargetRow As Integer = Math.Abs(SensorY - targetrow)
            Dim remainingManhattanDistance As Integer = manhattanDistance - distanceToTargetRow
            If remainingManhattanDistance < 0 Then
                Return Nothing
            End If
            Return New Range(SensorX - remainingManhattanDistance, SensorX + remainingManhattanDistance)
        End Function
    End Class

    Public Class Range
        Public First As Integer
        Public Last As Integer
        Public Sub New(f As Integer, l As Integer)
            First = f
            Last = l
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("({0}, {1})", First, Last)
        End Function

    End Class

End Class

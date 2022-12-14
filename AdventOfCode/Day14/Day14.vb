Public Class Day14

    'the greatest Y is 164 -> depth for part 2 is at 166
    Const WIDTH As Integer = 1000
    Const DEPTH As Integer = 200
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day14\input.txt")
        Dim Nodes(WIDTH, DEPTH) As Node

        'initialize the grid
        For i As Integer = 0 To WIDTH - 1
            For j As Integer = 0 To DEPTH - 1
                Nodes(i, j) = New Node()
            Next
        Next

        For Each line As String In lines
            Dim instructionStrings() As String = line.Split(" -> ")
            Dim rockLines As List(Of RockDrawingInstruction) = New List(Of RockDrawingInstruction)
            For Each instruction As String In instructionStrings
                rockLines.Add(New RockDrawingInstruction(instruction))
            Next
            AddRocksToMap(Nodes, rockLines)
        Next

        Dim sandInAbyss As Boolean = False
        Dim totalSand As Integer = 0
        While Not sandInAbyss
            sandInAbyss = DropSand(Nodes)
            totalSand += 1
        End While

        Console.WriteLine(totalSand - 1)
    End Sub

    Public Shared Sub calculate2()
        Dim lines As List(Of String) = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day14\input.txt").ToList()
        Dim Nodes(WIDTH, DEPTH) As Node

        'initialize the grid
        For i As Integer = 0 To WIDTH - 1
            For j As Integer = 0 To DEPTH - 1
                Nodes(i, j) = New Node()
            Next
        Next

        lines.Add("0,166 -> 999,166")

        For Each line As String In lines
            Dim instructionStrings() As String = line.Split(" -> ")
            Dim rockLines As List(Of RockDrawingInstruction) = New List(Of RockDrawingInstruction)
            For Each instruction As String In instructionStrings
                rockLines.Add(New RockDrawingInstruction(instruction))
            Next
            AddRocksToMap(Nodes, rockLines)
        Next

        Dim sandAtMax As Boolean = False
        Dim totalSand As Integer = 0
        While Not sandAtMax
            sandAtMax = DropSand(Nodes)
            totalSand += 1
        End While

        Console.WriteLine(totalSand - 1)
    End Sub

    Public Shared Sub AddRocksToMap(map(,) As Node, rocks As List(Of RockDrawingInstruction))
        Dim start As RockDrawingInstruction = Nothing
        Dim finish As RockDrawingInstruction = Nothing
        For Each instruction As RockDrawingInstruction In rocks
            finish = start
            start = instruction
            If finish Is Nothing Then
                Continue For
            End If

            Console.WriteLine("Drawing rocks from {0} to {1}", start, finish)

            If start.X = finish.X Then
                Dim minY As Integer = Math.Min(start.Y, finish.Y)
                Dim maxY As Integer = Math.Max(start.Y, finish.Y)
                For i As Integer = minY To maxY
                    map(start.X, i).Contents = Contents.Rock
                Next
            ElseIf start.Y = finish.Y Then
                Dim minX As Integer = Math.Min(start.X, finish.X)
                Dim maxX As Integer = Math.Max(start.X, finish.X)
                For i As Integer = minX To maxX
                    map(i, start.Y).Contents = Contents.Rock
                Next
            Else
                Console.WriteLine("Unexpected next instruction")
            End If
        Next
    End Sub

    Public Shared Function DropSand(map(,) As Node)
        Dim X As Integer = 500
        Dim Y As Integer = 0
        While True
            If map(X, Y + 1).Contents = Contents.Air Then
                Y += 1
            ElseIf map(X - 1, Y + 1).Contents = Contents.Air Then
                X -= 1
                Y += 1
            ElseIf map(X + 1, Y + 1).Contents = Contents.Air Then
                X += 1
                Y += 1
            Else
                map(X, Y).Contents = Contents.Sand
                If Y = 0 Then
                    Return True 'We filled up the chokepoint
                End If
                Return False 'We came to rest
            End If
            If Y = DEPTH - 2 Then
                Return 2 'we fell into the abyss
            End If
        End While
    End Function

    Public Class RockDrawingInstruction
        Public X As Integer
        Public Y As Integer
        Public Sub New(input As String)
            Dim tokens() = input.Split(",")
            X = Convert.ToInt32(tokens(0))
            Y = Convert.ToInt32(tokens(1))
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("({0}, {1})", X, Y)
        End Function
    End Class

    Public Enum Contents
        Air
        Rock
        Sand
    End Enum

    Public Class Node
        Public Contents As Contents

        Public Sub New()
            Contents = Contents.Air
        End Sub

        Public Overrides Function ToString() As String
            Return "x" 'String.Format("({0}, {1}) MS {2}", Column, Row, MinimumSteps)
        End Function

    End Class
End Class

Public Class Day12

    Const WIDTH As Integer = 113
    Const HEIGHT As Integer = 41

    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day12\input.txt")
        Dim Nodes(WIDTH, HEIGHT) As Node
        Dim row As Integer = 0
        Dim StartNode As Node
        Dim EndNode As Node
        Dim MininumHeightNodes As List(Of Node) = New List(Of Node)

        'process input
        For Each line As String In lines
            Dim column As Integer = 0
            For Each c As Char In line
                Nodes(column, row) = New Node(c, column, row)
                If c = "S"c Then
                    StartNode = Nodes(column, row)
                ElseIf c = "E"c Then
                    EndNode = Nodes(column, row)
                ElseIf c = "a"c Then
                    MininumHeightNodes.Add(Nodes(column, row))
                End If
                column += 1
            Next
            row += 1
        Next

        If StartNode Is Nothing Then
            Console.WriteLine("unknown start node??")
            StartNode = Nodes(0, 0)
        Else
            StartNode.MinimumSteps = 0
        End If

        Dim NodesToProcess As List(Of Node) = New List(Of Node)
        NodesToProcess.Add(StartNode)

        'Phase 2
        For Each node As Node In MininumHeightNodes
            node.MinimumSteps = 0
            NodesToProcess.Add(node)
        Next

        While (NodesToProcess.Count > 0)
            Dim NextGeneration As List(Of Node) = New List(Of Node)
            For Each node As Node In NodesToProcess
                NextGeneration.AddRange(ProcessNode(Nodes, node))
            Next
            NodesToProcess = NextGeneration
        End While

        Console.WriteLine(EndNode.MinimumSteps)
    End Sub

    'Returns the newly accessible nodes we should examine in the next generation
    Public Shared Function ProcessNode(nodes As Node(,), target As Node) As List(Of Node)
        Dim neighbors As List(Of Node) = New List(Of Node)
        Dim nodesToProcessNext As List(Of Node) = New List(Of Node)
        Dim column As Integer = target.Column
        Dim row As Integer = target.Row
        If column > 0 Then
            neighbors.Add(nodes(column - 1, row))
        End If
        If column < WIDTH - 1 Then
            neighbors.Add(nodes(column + 1, row))
        End If
        If row > 0 Then
            neighbors.Add(nodes(column, row - 1))
        End If
        If row < HEIGHT - 1 Then
            neighbors.Add(nodes(column, row + 1))
        End If
        For Each neighbor As Node In neighbors
            If neighbor.Height > target.Height + 1 Then
                Continue For 'Too steep
            End If
            If neighbor.MinimumSteps <= target.MinimumSteps + 1 Then
                Continue For 'There's already a way there
            End If
            ' Otherwise, we have a new faster way there
            neighbor.MinimumSteps = target.MinimumSteps + 1
            Console.WriteLine(String.Format("Found a new way to get to ({0}, {1}) of distance {2}", neighbor.Column, neighbor.Row, neighbor.MinimumSteps))
            nodesToProcessNext.Add(neighbor)
        Next
        Return nodesToProcessNext
    End Function

    Public Class Node
        Public Height As Integer
        Public MinimumSteps As Integer
        Public Row As Integer
        Public Column As Integer

        Public Sub New(h As Char, c As Integer, r As Integer)
            Column = c
            Row = r

            If h = "S"c Then
                Height = -1
            ElseIf h = "E"c Then
                Height = 26
            Else
                Height = Asc(h) - Asc("a"c)
            End If
            MinimumSteps = 9999 'Don't know how to get here yet
        End Sub

        Public Overrides Function ToString() As String
            Return String.Format("({0}, {1}) MS {2}", Column, Row, MinimumSteps)
        End Function

    End Class

End Class

Public Class Day13
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day13\input.txt")

        Dim runningTotal = 0
        'process input
        For i As Integer = 0 To lines.Count - 1 Step 3
            Dim result As Comparison = Compare(New DataNode(lines(i)), New DataNode(lines(i + 1)))
            If result = Comparison.Correct Then
                runningTotal += (i / 3) + 1
            End If
            Console.WriteLine(String.Format("{0}: {1}", (i / 3) + 1, result))
        Next
        Console.WriteLine(runningTotal)
    End Sub

    Public Shared Sub calculate2()
        Dim lines As List(Of String) = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day13\input.txt").ToList()
        lines.Add("[[2]]")
        lines.Add("[[6]]")

        Dim pairsList As List(Of StringNodePair) = New List(Of StringNodePair)
        For Each line As String In lines
            If line.Count > 0 Then
                Dim pair As StringNodePair
                pair.rep = line
                pair.node = New DataNode(line)
                pairsList.Add(pair)
            End If
        Next
        pairsList.Sort(Function(x, y) StringNodePairCompare(x, y))
        Dim i As Integer = 1
        For Each pair In pairsList
            Console.WriteLine(String.Format("{0}: {1}", i, pair.rep))
            i += 1
        Next
    End Sub

    Public Shared Function StringNodePairCompare(a As StringNodePair, b As StringNodePair) As Integer
        Dim result = Compare(a.node, b.node)
        Select Case result
            Case Comparison.Correct
                Return -1
            Case Comparison.Wrong
                Return 1
            Case Comparison.Inconclusive
                Return 0
        End Select
    End Function

    Public Structure StringNodePair
        Public rep As String
        Public node As DataNode
    End Structure

    Enum DataType
        DataList
        Number
    End Enum

    Enum Comparison
        Correct
        Wrong
        Inconclusive
    End Enum

    Public Shared Function Compare(left As DataNode, right As DataNode) As Comparison
        If left.Type = DataType.Number And right.Type = DataType.Number Then
            If left.Val < right.Val Then
                Return Comparison.Correct
            ElseIf right.Val < left.Val Then
                Return Comparison.Wrong
            Else
                Return Comparison.Inconclusive
            End If
        End If
        If left.Type = DataType.DataList And right.Type = DataType.DataList Then
            For i As Integer = 0 To Math.Min(left.DataList.Count - 1, right.DataList.Count - 1)
                If Compare(left.DataList(i), right.DataList(i)) <> Comparison.Inconclusive Then
                    Return Compare(left.DataList(i), right.DataList(i))
                End If
            Next
            'ok, we've reached the end and they were all equal so far
            If left.DataList.Count < right.DataList.Count Then
                Return Comparison.Correct
            ElseIf right.DataList.Count < left.DataList.Count Then
                Return Comparison.Wrong
            Else
                Return Comparison.Inconclusive
            End If
            Return Comparison.Inconclusive 'two 0-length lists
        End If
        'One is a list, one is a num
        If left.Type = DataType.Number Then
            Return Compare(DataNode.ListFromNumber(left.Val), right)
        End If
        'Only remaining possibility
        If right.Type = DataType.Number Then
            Return Compare(left, DataNode.ListFromNumber(right.Val))
        End If
        Console.WriteLine("Unexpected data types")
    End Function

    Public Class DataNode
        Public Type As DataType
        Public DataList As List(Of DataNode)
        Public Val As Integer

        Public Shared Function ListFromNumber(num As Integer) As DataNode
            Return New DataNode(String.Format("[{0}]", num))
        End Function

        Public Sub New(input As String)
            If input(0) = "["c Then
                ' It's a list
                Type = DataType.DataList
                DataList = New List(Of DataNode)
                Dim interior As String = input.Substring(1, input.Length - 2)
                'gotta tokenize based only off of commas that are not contained within lists
                Dim start As Integer = 0
                Dim unmatchedCount As Integer = 0
                For i = 0 To interior.Count - 1
                    Select Case interior(i)
                        Case "["c
                            unmatchedCount += 1
                        Case "]"c
                            unmatchedCount -= 1
                        Case ","
                            If unmatchedCount = 0 Then
                                DataList.Add(New DataNode(interior.Substring(start, i - start)))
                                start = i + 1
                            End If
                    End Select
                Next
                If interior.Count > 0 Then
                    DataList.Add(New DataNode(interior.Substring(start)))

                End If
            Else
                Type = DataType.Number
                Val = Convert.ToInt32(input)
            End If
        End Sub
    End Class
End Class

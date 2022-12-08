Public Class Day8

    Public Const SIZE As Integer = 99
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day8\input.txt")
        Dim runningTotal As Integer = 0
        Dim trees(SIZE, SIZE) As Node
        Dim i = 0 'line number
        For Each line As String In lines
            Dim j = 0 ' column number
            For Each item As Char In line
                trees(i, j) = New Node(Convert.ToInt32(item))
                j += 1
            Next
            i += 1
        Next

        FindVisibility(trees)
        Dim visibleTrees = CountVisibility(trees)

        Console.WriteLine(visibleTrees)
        Console.WriteLine(FindScenicScores(trees))
    End Sub

    Public Shared Sub FindVisibility(trees(,) As Node)
        ' Mark from the top first
        For i As Integer = 0 To SIZE - 1
            Dim MaxHeight As Integer
            trees(i, 0).Visible = True
            MaxHeight = trees(i, 0).Height
            For j As Integer = 1 To SIZE - 1
                If trees(i, j).Height > MaxHeight Then
                    trees(i, j).Visible = True
                    MaxHeight = trees(i, j).Height
                End If
            Next
            trees(i, SIZE - 1).Visible = True
            MaxHeight = trees(i, SIZE - 1).Height
            For j As Integer = SIZE - 2 To 1 Step -1
                If trees(i, j).Height > MaxHeight Then
                    trees(i, j).Visible = True
                    MaxHeight = trees(i, j).Height
                End If
            Next
        Next
        For j As Integer = 0 To SIZE - 1
            Dim MaxHeight As Integer
            trees(0, j).Visible = True
            MaxHeight = trees(0, j).Height
            For i As Integer = 1 To SIZE - 1
                If trees(i, j).Height > MaxHeight Then
                    trees(i, j).Visible = True
                    MaxHeight = trees(i, j).Height
                End If
            Next
            trees(SIZE - 1, j).Visible = True
            MaxHeight = trees(SIZE - 1, j).Height
            For i As Integer = SIZE - 2 To 1 Step -1
                If trees(i, j).Height > MaxHeight Then
                    trees(i, j).Visible = True
                    MaxHeight = trees(i, j).Height
                End If
            Next
        Next

    End Sub

    Public Shared Function FindScenicScores(trees(,) As Node) As Integer
        Dim MaxScenicScore As Integer = 0
        For i As Integer = 1 To SIZE - 2 'Ignore the outside, they're always going to be 0
            For j As Integer = 1 To SIZE - 2
                Dim ScenicScore As Integer = 1
                Dim iPos As Integer = 0
                Dim iNeg As Integer = 0
                Dim jPos As Integer = 0
                Dim jNeg As Integer = 0
                For iTest As Integer = i + 1 To SIZE - 1
                    iPos += 1
                    If trees(iTest, j).Height >= trees(i, j).Height Then
                        Exit For
                    End If
                Next
                For iTest As Integer = i - 1 To 0 Step -1
                    iNeg += 1
                    If trees(iTest, j).Height >= trees(i, j).Height Then
                        Exit For
                    End If
                Next
                For jTest As Integer = j + 1 To SIZE - 1
                    jPos += 1
                    If trees(i, jTest).Height >= trees(i, j).Height Then
                        Exit For
                    End If
                Next
                For jTest As Integer = j - 1 To 0 Step -1
                    jNeg += 1
                    If trees(i, jTest).Height >= trees(i, j).Height Then
                        Exit For
                    End If
                Next
                MaxScenicScore = Math.Max(MaxScenicScore, iPos * iNeg * jPos * jNeg)
            Next
        Next
        Return MaxScenicScore
    End Function

    Public Shared Function CountVisibility(trees(,) As Node) As Integer
        Dim VisibleTrees As Integer = 0
        For i As Integer = 0 To SIZE - 1
            For j As Integer = 0 To SIZE - 1
                If trees(i, j).Visible Then
                    VisibleTrees += 1
                    Console.WriteLine(trees(i, j).Height.ToString() + ": " + i.ToString() + ", " + j.ToString())
                End If
            Next
        Next
        Return VisibleTrees
    End Function


    Public Class Node
        Public Height As Integer
        Public Visible As Boolean
        Public Sub New(h As Integer)
            Height = h - Asc("0"c)
            Visible = False
        End Sub
    End Class

End Class

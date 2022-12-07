Public Class Day7
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day7\input.txt")
        Dim runningTotal As Integer = 0
        Dim tree As Dir = buildDirectory(lines)
        Dim allDirs = listAllDirectories(tree)
        ' Problem 1
        Console.WriteLine(allDirs.Where(Function(dir) dir.GetSize() <= 100000).Sum(Function(dir) dir.GetSize()))

        ' Problem 2
        Dim bigEnoughDirs As List(Of Dir) = allDirs.Where(Function(dir) 70000000 - tree.GetSize() + dir.GetSize() >= 30000000).ToList()
        bigEnoughDirs.Sort(Function(x, y) x.GetSize().CompareTo(y.GetSize))
        For Each dir As Dir In bigEnoughDirs
            Console.WriteLine(dir.Name + "  " + dir.GetSize().ToString())
        Next
        Console.WriteLine(allDirs.Where(Function(dir) dir.GetSize()))
    End Sub

    Private Shared Function buildDirectory(lines() As String) As Dir
        Dim root As Dir = New Dir(Nothing, "Root")
        Dim currentDirectory As Dir = root
        For Each line As String In lines
            Dim tokens() As String = line.Split(" ")
            If tokens(0) = "$" Then
                If tokens(1) = "cd" Then
                    If tokens(2) = ".." Then
                        currentDirectory = currentDirectory.Parent
                    Else
                        currentDirectory = currentDirectory.Children.Where(Function(dir) dir.Name = tokens(2)).First
                    End If
                End If
            ElseIf tokens(0) = "dir" Then
                currentDirectory.Children.Add(New Dir(currentDirectory, tokens(1)))
            Else
                currentDirectory.Files.Add(New File(tokens(1), tokens(0)))
            End If
        Next
        Return root
    End Function

    Private Shared Function listAllDirectories(root As Dir) As List(Of Dir)
        Dim dirs As List(Of Dir) = New List(Of Dir)
        dirs.Add(root)
        For Each child As Dir In root.Children
            For Each subchild In listAllDirectories(child)
                dirs.Add(subchild)
            Next
        Next
        Return dirs
    End Function

    Private Class File
        Public Name As String
        Public Size As Integer
        Public Sub New(n As String, s As Integer)
            Name = n
            Size = s
        End Sub
    End Class
    Private Class Dir
        Public Name As String
        Public Parent As Dir
        Public Children As List(Of Dir)
        Public Files As List(Of File)
        Public TotalSize As Integer

        Public Sub New(p As Dir, n As String)
            Parent = p
            Name = n
            Children = New List(Of Dir)
            Files = New List(Of File)
            TotalSize = -1
        End Sub

        Public Function GetSize() As Integer
            If TotalSize > -1 Then
                Return TotalSize
            Else
                TotalSize = Children.Sum(Function(dir) dir.GetSize()) + Files.Sum(Function(file) file.Size)
                Return TotalSize
            End If
        End Function
    End Class
End Class

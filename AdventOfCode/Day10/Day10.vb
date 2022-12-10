Public Class Day10
    Public Shared Sub calculate()
        Dim lines() As String = IO.File.ReadAllLines("C:\Users\Rob\source\repos\AdventOfCode\AdventOfCode\Day10\input.txt")
        Dim instructions As List(Of Instruction) = New List(Of Instruction)
        For Each line As String In lines
            Dim tokens() As String = line.Split()
            instructions.Add(New Instruction(tokens))
        Next

        Dim _cpu As CPU = New CPU
        For Each instruction As Instruction In instructions
            _cpu.ExecuteInstruction2(instruction)
        Next
        ' Console.WriteLine((_cpu.History(19) + _cpu.History(59) + _cpu.History(99) + _cpu.History(139) + _cpu.History(179) + _cpu.History(219)).ToString())

    End Sub

    Public Class CPU
        Public Cycle As Integer
        Public RegisterX As Integer
        Public History As List(Of Integer)
        Public Sub New()
            Cycle = 0
            RegisterX = 1
            History = New List(Of Integer)
        End Sub

        Public Sub ExecuteInstruction(instruction As Instruction)
            Select Case instruction.InstructionType
                Case InstructionType.Noop
                    Cycle += 1
                    History.Add(Cycle * RegisterX)
                    Console.WriteLine(Cycle.ToString() + Microsoft.VisualBasic.vbTab + RegisterX.ToString() + Microsoft.VisualBasic.vbTab + (Cycle * RegisterX).ToString())
                Case InstructionType.Add
                    Cycle += 1
                    History.Add(Cycle * RegisterX)
                    Console.WriteLine(Cycle.ToString() + Microsoft.VisualBasic.vbTab + RegisterX.ToString() + Microsoft.VisualBasic.vbTab + (Cycle * RegisterX).ToString())
                    Cycle += 1
                    History.Add(Cycle * RegisterX)
                    Console.WriteLine(Cycle.ToString() + Microsoft.VisualBasic.vbTab + RegisterX.ToString() + Microsoft.VisualBasic.vbTab + (Cycle * RegisterX).ToString())
                    RegisterX += instruction.Value
            End Select
        End Sub

        Public Sub ExecuteInstruction2(instruction As Instruction)
            Select Case instruction.InstructionType
                Case InstructionType.Noop
                    Cycle += 1
                    DrawCharacter()
                Case InstructionType.Add
                    Cycle += 1
                    DrawCharacter()
                    Cycle += 1
                    DrawCharacter()
                    RegisterX += instruction.Value
            End Select
        End Sub

        Public Sub DrawCharacter()
            If Math.Abs(RegisterX - (Cycle - 1) Mod 40) <= 1 Then
                Console.Write("#")
            Else
                Console.Write(".")
            End If
            If Cycle Mod 40 = 0 Then
                Console.WriteLine()
            End If
        End Sub

    End Class

    Enum InstructionType
        Noop
        Add
    End Enum

    Public Class Instruction
        Public InstructionType As InstructionType
        Public Value As Integer
        Public Sub New(tokens() As String)
            If (tokens(0) = "addx") Then
                InstructionType = InstructionType.Add
                Value = Convert.ToInt32(tokens(1))
            Else
                InstructionType = InstructionType.Noop
                Value = 0
            End If
        End Sub
    End Class

End Class

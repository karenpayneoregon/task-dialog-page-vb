Namespace Classes
    Module Extensions
        <Runtime.CompilerServices.Extension>
        Public Function ToYesNoString(value As Boolean) As String
            Return If(value, "Yes", "No")
        End Function

    End Module
End Namespace
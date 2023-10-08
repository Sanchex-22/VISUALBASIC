Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class Form1
    Dim segundosRestantes As Integer = 60 ' Inicializar con 60 segundos
    Dim resultado As Double
    'Dim resultado_user As Double
    Dim elementosGenerados As String
    ' Dim botones() As Button = {Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8}
    Dim operadores() As String = {"+", "-", "*", "/"}
    'Dim datos() As String
    Dim valores As New List(Of String)
    'Dim datos As New List(Of String)()

    Public Sub New()
        InitializeComponent()
        btnSiguiente.Enabled = False
        btnOffNum()
        btnOffOp()
        btnEnviar.Enabled = False
        txtInput.ReadOnly = True
        TextBox1.ReadOnly = True
    End Sub

    Public Sub btnOnNum()
        Button1.Enabled = True
        Button3.Enabled = True
        Button5.Enabled = True
        Button7.Enabled = True
    End Sub

    Public Sub btnOffNum()
        Button1.Enabled = False
        Button3.Enabled = False
        Button5.Enabled = False
        Button7.Enabled = False
    End Sub

    Public Sub btnOnOp()
        Button2.Enabled = True
        Button4.Enabled = True
        Button6.Enabled = True
        Button8.Enabled = True
    End Sub

    Public Sub btnOffOp()
        Button2.Enabled = False
        Button4.Enabled = False
        Button6.Enabled = False
        Button8.Enabled = False
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Configurar el intervalo del temporizador (1000 ms = 1 segundo)
        Timer1.Interval = 1000
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btn_Generar.Click
        Timer1.Start()
        Comenzar()
        btn_Generar.Enabled = False
    End Sub

    Public Sub ActiveDisablebtn(btns As String)
        Dim btn As String = btns
        If btn = "0" Or btn = "1" Or btn = "2" Or btn = "3" Or btn = "4" Or btn = "5" Or btn = "6" Or btn = "7" Or btn = "8" Or btn = "9" Then
            btnOffNum()
            btnOnOp()
        ElseIf btn = "+" Or btn = "*" Or btn = "-" Or btn = "/" Then
            btnOffOp()
            btnOnNum()
        End If
    End Sub

    'funcion para que empieze el juego
    Public Sub Comenzar()
        ' Crear un arreglo para almacenar números y operadores intercalados
        Dim elementos(7) As Object
        Dim generadorAleatorio As New Random()

        ' Generar 4 números aleatorios y 3 operadores aleatorios
        For i As Integer = 0 To 3
            elementos(i * 2) = generadorAleatorio.Next(1, 9) ' Números aleatorios entre 1 y 100
            ' Generar operadores aleatorios
            elementos(i * 2 + 1) = operadores(generadorAleatorio.Next(0, operadores.Length))
        Next

        ' Mostrar los números y operadores en un cuadro de texto o en la consola
        elementosGenerados = String.Join(" ", elementos.Take(7))
        txtElementos.Text = elementosGenerados

        ' Llenar los botones con datos generados
        Button1.Text = elementos(0)
        Button3.Text = elementos(2)
        Button5.Text = elementos(4)
        Button7.Text = elementos(6)
        Button2.Text = operadores(0)
        Button4.Text = operadores(1)
        Button6.Text = operadores(2)
        Button8.Text = operadores(3)
        btnOnNum()
        ' btnOnOp()
        btnEnviar.Enabled = True
        ' Realizar la operación matemática y guardar el resultado
        resultado = CalcularResultado(elementos)
        txtResultado.Text = resultado.ToString()
    End Sub

    ' Función para calcular el resultado de la operación matemática
    Private Function CalcularResultado(elementos As Object()) As Double
        ' Guardar los elementos en variables individuales
        Dim num1 As Double = Convert.ToDouble(elementos(0))
        Dim operador1 As String = Convert.ToString(elementos(1))
        Dim num2 As Double = Convert.ToDouble(elementos(2))
        Dim operador2 As String = Convert.ToString(elementos(3))
        Dim num3 As Double = Convert.ToDouble(elementos(4))
        Dim operador3 As String = Convert.ToString(elementos(5))
        Dim num4 As Double = Convert.ToDouble(elementos(6))


        ' Realizar la operación en el orden especificado
        Dim resultado1 As Double
        Dim resultado2 As Double
        Dim resultadoma As Double

        resultado1 = Calculo(num1, num2, operador1)
        res1.Text = resultado1.ToString()
        resultado2 = Calculo(num3, num4, operador3)
        res2.Text = resultado2.ToString()
        resultadoma = Calculo(resultado1, resultado2, operador2)

        Return resultadoma
    End Function

    Public Function Calculo(num1 As Double, num2 As Double, operador As String)
        Dim resultado As Double
        If operador = "/" Then
            resultado = num1 / num2
            Return resultado
        ElseIf operador = "*" Then
            resultado = num1 * num2
            Return resultado
        ElseIf operador = "-" Then
            resultado = num1 - num2
            Return resultado
        ElseIf operador = "+" Then
            resultado = num1 + num2
            Return resultado
        Else
            Console.WriteLine("El operador no es válido.")
        End If
        Return resultado
    End Function

    Private Sub Timer1_Tick1(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Actualizar el contador de segundos restantes
        segundosRestantes -= 1

        ' Mostrar el tiempo restante en un control TextBox (o donde desees)
        TextBox1.Text = segundosRestantes.ToString() & " segundos"

        ' Comprobar si el tiempo ha llegado a cero
        If segundosRestantes = 0 Then
            ' El tiempo ha terminado, puedes tomar acciones aquí
            Timer1.Stop() ' Detener el temporizador si es necesario
            MessageBox.Show("¡Tiempo agotado!")
        End If
    End Sub

    Private Sub Actualizar()
        txtInput.Text = String.Join(", ", valores)
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        segundosRestantes = segundosRestantes + 60
        Comenzar()
        btnSiguiente.Enabled = True
    End Sub


    '--------------------BOTONES--------------------
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim valorDelBoton As String = DirectCast(sender, Windows.Forms.Button).Text
        If valores.Count < 7 Then
            'MessageBox.Show(valores.Count)
            valores.Add(valorDelBoton)
            ' Puedes realizar acciones adicionales si lo deseas, como mostrar los valores en un TextBox
            txtInput.Text = String.Join(", ", valores)
            Actualizar()
            ActiveDisablebtn(valorDelBoton)
        Else
            MessageBox.Show("La lista ya contiene 8 elementos. No se pueden agregar más.")
            btnOffOp()
            btnOffNum()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim valorDelBoton As String = DirectCast(sender, Windows.Forms.Button).Text
        If valores.Count < 7 Then
            'MessageBox.Show(valores.Count)
            valores.Add(valorDelBoton)
            ' Puedes realizar acciones adicionales si lo deseas, como mostrar los valores en un TextBox
            txtInput.Text = String.Join(", ", valores)
            Actualizar()
            ActiveDisablebtn(valorDelBoton)
        Else
            MessageBox.Show("La lista ya contiene 8 elementos. No se pueden agregar más.")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim valorDelBoton As String = DirectCast(sender, Windows.Forms.Button).Text
        If valores.Count < 7 Then
            valores.Add(valorDelBoton)
            ' Puedes realizar acciones adicionales si lo deseas, como mostrar los valores en un TextBox
            txtInput.Text = String.Join(", ", valores)
            Actualizar()
            ActiveDisablebtn(valorDelBoton)
        Else
            MessageBox.Show("La lista ya contiene 8 elementos. No se pueden agregar más.")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim valorDelBoton As String = DirectCast(sender, Windows.Forms.Button).Text
        If valores.Count < 7 Then
            valores.Add(valorDelBoton)
            ' Puedes realizar acciones adicionales si lo deseas, como mostrar los valores en un TextBox
            txtInput.Text = String.Join(", ", valores)
            Actualizar()
            ActiveDisablebtn(valorDelBoton)
        Else
            MessageBox.Show("La lista ya contiene 8 elementos. No se pueden agregar más.")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim valorDelBoton As String = DirectCast(sender, Windows.Forms.Button).Text
        If valores.Count < 7 Then
            valores.Add(valorDelBoton)
            ' Puedes realizar acciones adicionales si lo deseas, como mostrar los valores en un TextBox
            txtInput.Text = String.Join(", ", valores)
            Actualizar()
            ActiveDisablebtn(valorDelBoton)
        Else
            MessageBox.Show("La lista ya contiene 8 elementos. No se pueden agregar más.")
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim valorDelBoton As String = DirectCast(sender, Windows.Forms.Button).Text
        If valores.Count < 7 Then
            valores.Add(valorDelBoton)
            ' Puedes realizar acciones adicionales si lo deseas, como mostrar los valores en un TextBox
            txtInput.Text = String.Join(", ", valores)
            Actualizar()
            ActiveDisablebtn(valorDelBoton)
        Else
            MessageBox.Show("La lista ya contiene 8 elementos. No se pueden agregar más.")
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim valorDelBoton As String = DirectCast(sender, Windows.Forms.Button).Text
        If valores.Count < 7 Then
            valores.Add(valorDelBoton)
            ' Puedes realizar acciones adicionales si lo deseas, como mostrar los valores en un TextBox
            txtInput.Text = String.Join(", ", valores)
            Actualizar()
            ActiveDisablebtn(valorDelBoton)
        Else
            MessageBox.Show("La lista ya contiene 8 elementos. No se pueden agregar más.")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim valorDelBoton As String = DirectCast(sender, Windows.Forms.Button).Text
        If valores.Count < 7 Then
            valores.Add(valorDelBoton)
            ' Puedes realizar acciones adicionales si lo deseas, como mostrar los valores en un TextBox
            txtInput.Text = String.Join(", ", valores)
            Actualizar()
            ActiveDisablebtn(valorDelBoton)
        Else
            MessageBox.Show("La lista ya contiene 8 elementos. No se pueden agregar más.")
        End If
    End Sub

    Private Sub btnBorrarTodo_Click(sender As Object, e As EventArgs) Handles btnBorrarTodo.Click
        If valores.Count > 0 Then
            ' Eliminar todos los elementos de la lista
            valores.Clear()

            ' Actualizar el TextBox para mostrar la lista vacía
            btnOffOp()
            btnOnNum()
            Actualizar()
        Else
            MessageBox.Show("No hay valores para eliminar.")
        End If
    End Sub

    'Boton que borra el ultimo elemento del array
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        If valores.Count > 0 Then
            ' Eliminar el último valor agregado
            ActiveDisablebtnDelete(valores.Last())
            valores.RemoveAt(valores.Count - 1)
            ' Actualizar el TextBox para mostrar los valores actualizados
            Actualizar()
        Else
            MessageBox.Show("No hay valores para eliminar.")
        End If
    End Sub

    Public Sub ActiveDisablebtnDelete(btns As String)
        Dim btn As String = btns
        If btn = "0" Or btn = "1" Or btn = "2" Or btn = "3" Or btn = "4" Or btn = "5" Or btn = "6" Or btn = "7" Or btn = "8" Or btn = "9" Then
            btnOffOp()
            btnOnNum()
        ElseIf btn = "+" Or btn = "*" Or btn = "-" Or btn = "/" Then
            btnOnOp()
            btnOffNum()
        End If
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        If valores.Count > 6 Then
            Comparar()
        Else
            MessageBox.Show(valores.Count)
            MessageBox.Show("Ingrese todos los valores")
        End If
    End Sub

    Public Function Comparar()
        MessageBox.Show("Calculando...")
        Dim array() As String = valores.ToArray()
        Dim resultado_user = CalcularResultado(array)
        txtUserResult.Text = resultado_user
        If resultado = resultado_user Then
            btnSiguiente.Enabled = True
            MessageBox.Show("Resultado Correcto (Presione Siguiente)")
        Else
            MessageBox.Show("Resultado erroneo Intente Denuevo")
        End If
    End Function


    Public Function Limpiar()

    End Function
End Class

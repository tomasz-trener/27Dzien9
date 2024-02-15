<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kalkulator.aspx.cs" Inherits="P01AplikacjaWebowaWstep.Kalkulator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      
        <p>Liczba 1</p>
        <asp:TextBox ID="txtLiczba1" runat="server"></asp:TextBox>

        <p>Liczba 2</p>
        <asp:TextBox ID="txtLiczba2" runat="server"></asp:TextBox>

        <br />
        <asp:Button ID="btnPolicz" OnClick="btnPolicz_Click" runat="server" Text="Policz" />

        <p>Wynik</p>
        <asp:Label ID="lblWynik" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>

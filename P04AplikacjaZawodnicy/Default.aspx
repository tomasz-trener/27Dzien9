<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="P04AplikacjaZawodnicy.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       

        <table>
            <tr>
                <th>Imie</th>
                <th>Nazwisko</th>
                <th>Kraj</th>
                <th>Data Urodzenia</th>
            </tr>
            
            <%
                foreach (var z in Zawodnicy)
                { %>

                <tr>
                    <td><%= z.Imie %></td>
                    <td><%= z.Nazwisko %></td>
                    <td><%= z.Kraj %></td>
                    <td><%= z.DataUrodzenia?.ToString("dd-MM-yyyy") %></td>
                </tr>


           <%   }
                %>

        </table>



    </form>
</body>
</html>

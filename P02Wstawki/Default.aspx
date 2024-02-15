<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="P02Wstawki.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        

        <%

            string s = "ala ma kota";
            s = s.ToUpper();

            Response.Write(s);

            %>


        <br />

        <%

            for (int i = 0; i < 10; i++)
            {
                // Response.Write("<p>" + i + "</p>"); unikamy takiego rozwiazania
  %>

                 <%--   <p> <% Response.Write(i); %> </p>--%>
                        <p><%= i %></p> <%--Bardziej zalecana forma skrconego wypisywania tekstu --%>


 <%
            }

            %>

          <%

              foreach (var imie in Imiona)
              { %>

                  <p><b><%= imie %></b></p>

        <%     }
              %>



    </form>
</body>
</html>

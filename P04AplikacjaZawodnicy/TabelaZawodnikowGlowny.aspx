<%@ Page Title="" Language="C#" MasterPageFile="~/Glowny.Master" AutoEventWireup="true" CodeBehind="TabelaZawodnikowGlowny.aspx.cs" Inherits="P04AplikacjaZawodnicy.TabelaZawodnikowGlowny" %>



<asp:Content ID="Content1" ContentPlaceHolderID="glownyObszar" runat="server">


      <div class="row">
    <div class="col-md-12">
      <div class="card">
        <div class="card-header">
          <h4 class="card-title"> Zawodnicy</h4>
        </div>

          <div style="margin-left:15px">
              <a href="SzczegolyZawodnikaGlowny.aspx">Stworz nowy rekord</a>
          </div>


        <div class="card-body">
          <div class="table-responsive">
            <table class="table">
              <thead class=" text-primary">
                <th>
                  Nazwa
                </th>
                <th>
                  Kraj
                </th>
                <th>
                  Data urodzenia
                </th>
                <th>
                  Wzrost
                </th>
                  <th>
                  Waga
                  </th>
              </thead>
              <tbody>
               
                  <%
                      foreach (var z in Zawodnicy)
                      { %>

                          <tr <%= z.Id_zawodnika== IdPodswietlanego ? "class=\"podswietlony\"" : "" %>>
                              <td><a href="SzczegolyZawodnikaGlowny.aspx?id=<%= z.Id_zawodnika %>"> <%= z.ImieNazwisko %></a></td>
                              <td><%= z.Kraj %></td>
                              <td><%= z.DataUrodzenia?.ToString("dd-MM-yyyy") %></td>
                              <td><%= z.Wzrost %></td>
                              <td><%= z.Waga %></td>
                          </tr>

                   <%   }
                      %>

              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
 
  </div>


</asp:Content>

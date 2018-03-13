<%@ Page Language="C#" AutoEventWireup="true" CodeFile="proofOfConcept.aspx.cs" Inherits="proofOfConcept" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>PSPICE</title>
      <link rel="Stylesheet" type="text/css" href="StyleSheet2.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <h1> Wyniki symulacji przeprowadzonej w programie PSPICE 
        </h1>
         <asp:Button ID="BackButton" runat="server" Text="Powrót" OnClick="BackButton_Click"/>
        <table class="center" border="1"><tr>

    <td colspan="3">
       <asp:Label ID="lab14" runat="server" Text="Dane symulacji:" CssClass="tekst1"></asp:Label>
    </td>
    </tr>
    <tr>
    <td> <asp:Label ID="lab1" runat="server" Text="Amplituda:" CssClass="tekst1"></asp:Label>
    </td>
        <td><asp:Label ID="lab2" runat="server" Text="100 V" CssClass="tekst1"></asp:Label></td></tr>
              <tr>  <td> <asp:Label ID="lab5" runat="server" Text="Częstotliwość:" CssClass="tekst1"></asp:Label>
    </td>
        <td><asp:Label ID="lab6" runat="server" Text="50 Hz" CssClass="tekst1"></asp:Label></td></tr>

                        
            <tr>
    <td> <asp:Label ID="lab3" runat="server" Text="R1:" CssClass="tekst1"></asp:Label>
    </td>
        <td><asp:Label ID="lab4" runat="server" Text="1000 Ω" CssClass="tekst1"></asp:Label></td></tr>

                        <tr>

    <td> <asp:Label ID="lab7" runat="server" Text="R2:" CssClass="tekst1"></asp:Label>
    </td>
        <td><asp:Label ID="lab8" runat="server" Text="10000 Ω" CssClass="tekst1"></asp:Label></td></tr>

 <tr>

    <td> <asp:Label ID="lab9" runat="server" Text="C:" CssClass="tekst1"></asp:Label>
    </td>
        <td><asp:Label ID="lab10" runat="server" Text="0,000000003 F" CssClass="tekst1"></asp:Label></td></tr>
                            
             <tr>

    <td> <asp:Label ID="lab11" runat="server" Text="L:" CssClass="tekst1"></asp:Label>
    </td>
        <td><asp:Label ID="lab12" runat="server" Text="0,000003 H" CssClass="tekst1"></asp:Label></td></tr>
</table> <br />
        <h2>Widmo amplitudowe</h2><br />
      <asp:Image ID="magnitude2" runat="server" ImageUrl="~/magnitude_pspice.png" CssClass="obwod" ToolTip="Widmo amplitudowe" />
        <br /><br />
         <h2>Widmo fazowe</h2><br />
          <asp:Image ID="phase2" runat="server" ImageUrl="~/phase_pspice.png" CssClass="obwod" ToolTip="Widmo fazowe" />
        <br /><br />
         <h2>Obwiednia prądu zasilającego</h2><br />
          <asp:Image ID="current2" runat="server" ImageUrl="~/current_pspice.png" CssClass="obwod" ToolTip="Obwiednia prądu zasilającego" />
    </div>
    </form>
</body>
</html>

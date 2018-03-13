<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="main" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Filtr bierny II rzędu</title>
    <link rel="Stylesheet" type="text/css" href="StyleSheet.css" />

</head>
<body>
    <form id="form1" runat="server">

    <div>
        <h1> Aplikacja internetowa ASP.NET zaprogramowana w środowisku Visual Studio. 
        </h1>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/circuit.png" CssClass="obwod" ToolTip="Obwód filtra" />
    <br />
                <asp:Button ID="Credits2" runat="server" Text="PSPICE" OnClick="Credits2_Click" />
        <asp:Button ID="Credits" runat="server" Text="O Autorze" OnClick="Credits_Click"/>
     
         <div class="container">

        <asp:Panel ID="Panel1" runat="server" GroupingText="Parametry funkcji">
<table class="center"><tr>

    <td colspan="3">
        <p>Parametry zasilania:
        </p>
    </td>
    </tr>
    <tr>
    <td> <asp:Label ID="labMagnitude" runat="server" Text="Amplituda:" CssClass="tekst1"></asp:Label>
    </td>
    <td><asp:TextBox ID="txtMagnitude" runat="server" Text="100,0"></asp:TextBox></td><td class="auto-style1">

        <asp:DropDownList ID="dropDownList1" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="V" Value="1"></asp:ListItem>
    <asp:ListItem Text="mV" Value="2"></asp:ListItem>
    <asp:ListItem Text="kV" Value="3"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>

     <tr>
    <td> <asp:Label ID="labFrequency" runat="server" Text="Częstotliwość:" CssClass="tekst1"></asp:Label>
    </td>
    <td><asp:TextBox ID="txtFrequency" runat="server" Text="50,0"></asp:TextBox></td>
         <td class="auto-style1">

        <asp:DropDownList ID="dropDownList2" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="Hz" Value="1"></asp:ListItem>
    <asp:ListItem Text="kHz" Value="2"></asp:ListItem>
    <asp:ListItem Text="Mhz" Value="3"></asp:ListItem>
    </asp:DropDownList></td>

    </tr>  

         <tr>
    <td>  <p>Parametry elementów:</p>
   </td>
    </tr>

         <tr>
    <td> <asp:Label ID="labResistance1" runat="server" Text="Rezystor R1:" CssClass="tekst1"></asp:Label>
    </td>
    <td> <asp:TextBox ID="txtRes1" runat="server" Text="1000"></asp:TextBox></td>

                      <td class="auto-style1">

        <asp:DropDownList ID="dropDownList3" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="Ω" Value="1"></asp:ListItem>
    <asp:ListItem Text="kΩ" Value="2"></asp:ListItem>
    <asp:ListItem Text="MΩ" Value="3"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>

             <tr>
    <td><asp:Label ID="labResistance2" runat="server" Text="Rezystor R2:" CssClass="tekst1"></asp:Label>
    </td>
    <td> <asp:TextBox ID="txtRes2" runat="server" Text="10000"></asp:TextBox></td>

                          <td class="auto-style1">


        <asp:DropDownList ID="dropDownList4" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="Ω" Value="1"></asp:ListItem>
    <asp:ListItem Text="kΩ" Value="2"></asp:ListItem>
    <asp:ListItem Text="MΩ" Value="3"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>


                 <tr>
    <td><asp:Label ID="labCapacity" runat="server" Text="Pojemność C:" CssClass="tekst1"></asp:Label>
    </td>
    <td>  <asp:TextBox ID="txtCap" runat="server" Text="0,000000003"></asp:TextBox></td>

                              <td class="auto-style1">


        <asp:DropDownList ID="dropDownList5" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="F" Value="1"></asp:ListItem>
    <asp:ListItem Text="mF" Value="2"></asp:ListItem>
    <asp:ListItem Text="µF" Value="3"></asp:ListItem>
    <asp:ListItem Text="pF" Value="4"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>

                     <tr>
    <td> <asp:Label ID="labInductance" runat="server" Text="Indukcyjność L:" CssClass="tekst1"></asp:Label>
    </td>
    <td>  <asp:TextBox ID="txtInd" runat="server" Text="0,000003"></asp:TextBox></td>

                                  <td class="auto-style1">


        <asp:DropDownList ID="dropDownList6" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="H" Value="1"></asp:ListItem>
    <asp:ListItem Text="mH" Value="2"></asp:ListItem>
    <asp:ListItem Text="µH" Value="3"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>

             <tr>
    <td>  <p>Parametry symulacji:</p>
   </td>
    </tr>

                         <tr>
    <td>  <asp:Label ID="labFreqStart" runat="server" Text="Częstotliwość początkowa:" CssClass="tekst1"></asp:Label>
    </td>
    <td>   <asp:TextBox ID="txtFreqStart" runat="server" Text="1"></asp:TextBox></td>

                                      <td class="auto-style1">

        <asp:DropDownList ID="dropDownList7" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="Hz" Value="1"></asp:ListItem>
    <asp:ListItem Text="kHz" Value="2"></asp:ListItem>
    <asp:ListItem Text="Mhz" Value="3"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>

                             <tr>
    <td>  <asp:Label ID="labFreqEnd" runat="server" Text="Częstotliwość końcowa:" CssClass="tekst1"></asp:Label>
    </td>
    <td>  <asp:TextBox ID="txtFreqEnd" runat="server" Text="10000000"></asp:TextBox></td>

                                          <td class="auto-style1">


        <asp:DropDownList ID="dropDownList8" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="Hz" Value="1"></asp:ListItem>
    <asp:ListItem Text="kHz" Value="2"></asp:ListItem>
    <asp:ListItem Text="Mhz" Value="3"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>
   
      <tr>
    <td>  <asp:Label ID="labSamples" runat="server" Text="Liczba próbek:" CssClass="tekst1"></asp:Label>
    </td>
    <td>  <asp:TextBox ID="txtSamples" runat="server" Text="140000"></asp:TextBox></td>

                                          <td class="auto-style1">


        <asp:DropDownList ID="dropDownList9" runat="server" CssClass="dDownList">
         <asp:ListItem Enabled="true" Text="S" Value="1"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>
</table>
             <br /><br />

 <asp:Button ID="btn1" runat="server" Text="Widmo amplitudowe" OnClick="btn1_Click" />
         <asp:Button ID="btn2" runat="server" Text="Widmo fazowe" OnClick="btn2_Click"/>
         <asp:Button ID="btn3" runat="server" Text="Obwiednia prądu" OnClick="btn3_Click"/>
          <asp:Button ID="btnClear" runat="server" Text="Wyczyść parametry" OnClick="btnClear_Click" />
                    

        </asp:Panel>
             &nbsp <br />
                <asp:Panel ID="Panel2" runat="server" GroupingText="Log" CssClass="logPanel">
                            <asp:Label ID="labMessage" runat="server" ForeColor="Fuchsia">
        </asp:Label>
                </asp:Panel>
             </div>
       
        <br />
        
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server"> 
                       <div align="center">
                    <asp:Panel ID="panWaveform" runat="server" GroupingText="Widmo amplitudowe" CssClass="charts">
        <asp:Chart ID="Chart1" runat="server" Height="473px" Width="1280px">
            <series>
                <asp:Series ChartType="Line" Name="Series1" Legend="Legend1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
            <Legends>
                <asp:Legend Name="Legend1"></asp:Legend>
            </Legends>
        </asp:Chart>
    </asp:Panel>
                            </div>
                    </asp:View>

                     <asp:View ID="View2" runat="server"> 
                          <div align="center">
                      <asp:Panel ID="panWaveform2" runat="server" GroupingText="Widmo fazowe" CssClass="charts">
        <asp:Chart ID="Chart2" runat="server" Height="473px" Width="1280px">
            <series>
                <asp:Series ChartType="Line" Name="Series1" Legend="Legend1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
            <Legends>
                <asp:Legend Name="Legend1"></asp:Legend>
            </Legends>
        </asp:Chart>
    </asp:Panel>
                              </div>
                     </asp:View>

                     <asp:View ID="View3" runat="server"> 
                     <div align="center">
                 <asp:Panel ID="panWaveform3" runat="server" GroupingText="Obwiednia prądu zasilającego" CssClass="charts">
        <asp:Chart ID="Chart3" runat="server" Height="473px" Width="1280px">
            <series>
                <asp:Series ChartType="Line" Name="Series1" Legend="Legend1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
            <Legends>
                <asp:Legend Name="Legend1"></asp:Legend>
            </Legends>
        </asp:Chart>
    </asp:Panel>
                         </div>
                     </asp:View>
                
            </asp:MultiView>

    </div>
    </form>
</body>

</html>

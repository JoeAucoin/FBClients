<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditClient.ascx.cs" Inherits="GIBS.Modules.FBClients.EditClient" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register tagprefix="dnn" Assembly="DotNetNuke.WebControls" Namespace="DotNetNuke.UI.WebControls" %>

 <dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FBClients/Style.css?1=3" />
<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/smoothness/jquery-ui.css" />

<style>
.sigZoom {
  padding: 0px;
  
  transition: transform .2s; /* Animation */
  width: 150px;
  height: 30px;
  margin: 0 auto;
}

.sigZoom:hover {
  transform: scale(3.3); /* (150% zoom - Note: if the zoom is too large, it will go outside of the viewport) */
}
</style>



<script type="text/javascript" >

 
    
    function ValidateCheckBox(sender, args) {
        if (document.getElementById("<%=cbxAFMDOB_Verified.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    } 


    var objChkd;
    function HandleOnCheck() {
        var chkLst = document.getElementById('<%= cblClientDisability.ClientID %>');
        if (objChkd && objChkd.checked)
            objChkd.checked = false; objChkd = event.srcElement;
    }



    $(function () {
        $("#txtClientDOB").datepicker({
            numberOfMonths: 1,
            changeYear: true,
            yearRange: '-99:-16',
            showButtonPanel: false,
            showCurrentAtPos: 0,
            maxDate: "-16y",
            minDate: "-99y"

        });

        $("#txtVerifyDate").datepicker({
            numberOfMonths: 1,

            showButtonPanel: false,
            showCurrentAtPos: 0
        });

        $("#txtClientAddressVerifyDate").datepicker({
            numberOfMonths: 1,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });

        $("#txtAFM_DOB").datepicker({
            numberOfMonths: 1,
            changeYear: true,
            yearRange: '-99:+0',
            showButtonPanel: false,
            showCurrentAtPos: 0
           
        });

    });


    //$('Form').dirtyForms();



    jQuery(function ($) { $("#<%= txtPhone.ClientID %>").mask("(999) 999-9999"); });
    jQuery(function ($) { $("#<%= txtZip.ClientID %>").mask("99999?-9999"); });

    // BIND DNN Tabs
    jQuery(function ($) { $('#tabs-client').dnnTabs(); });


    jQuery(function ($) {
        $("#<%= txtIeAmount.ClientID %>").Watermark("000.00");
        $("#<%= txtIeExpenseAmount.ClientID %>").Watermark("000.00");
      //  $("#<%= txtClientFirstName.ClientID %>").Watermark("First");    
        //     $("#<%= txtClientMiddleInitial.ClientID %>").Watermark("MI");
     //   $("#<%= txtClientLastName.ClientID %>").Watermark("Last");
        //     $("#<%= txtZip.ClientID %>").Watermark("00000");
        //     $("#<%= txtPhone.ClientID %>").Watermark("(508) 555-5555");

    });


    jQuery(function ($) {
        $("#txtBikeAwardedDate").datepicker({
            numberOfMonths: 1,
            dateFormat: 'mm/dd/yy',
            showButtonPanel: false,
            showCurrentAtPos: 0
        });
        //txtReceivedToysDate
        $("#txtReceivedToysDate").datepicker({
            numberOfMonths: 1,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });

    });


    $(function () {
        $("#txtVisitDate").datepicker({
            numberOfMonths: 1,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });

    });

    function UseData() {
        //  requireClientVerifyDate();
        //  requireAddressVerifyDate();
        $.Watermark.HideAll();   //Do Stuff   $.Watermark.ShowAll();
    }

    dataChanged = 0;     // global variable flags unsaved changes      


    function DisableCheck()
    {
        dataChanged = 0;
    
    }

    function bindForChange() {

        $('input,checkbox,textarea,radio').bind('change', function (event) { dataChanged = 1 })
        $(':reset,:submit').bind('click', function (event) { dataChanged = 0 })
    }


    function askConfirm() {
        if (dataChanged) {
            return "You have some unsaved changes.\nSelect STAY ON THIS PAGE\nand update the record."
        }
    }

    window.onbeforeunload = askConfirm;
    window.onload = bindForChange;

    $(function () {
        $("#dialog").dialog({
            autoOpen: false
        });
        $("#button").on("click", function () {
            $("#dialog").dialog("open");
        });
    });

    jQuery(function ($) {
        var setupModule = function () {
            $('#tabs-client').dnnPanels();
            $('#tabs-client .dnnFormExpandContent a').dnnExpandAll({
                targetArea: '#tabs-client'
            });
        };
        setupModule();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            // note that this will fire when _any_ UpdatePanel is triggered,
            // which may or may not cause an issue
            setupModule();
        });
    });



    setTimeout(function () {
        $('input').attr('autocomplete', 'off');
    }, 2000);

    // SIGNATURE JAVASCRIPT BELOW
    // SIGNATURE JAVASCRIPT BELOW
    // SIGNATURE JAVASCRIPT BELOW
    // SIGNATURE JAVASCRIPT BELOW

 
    //btnAddClientVisit

    var tmr;

    function onSign() {
      //  alert("here");
        if (IsSigWebInstalled()) {
        //    alert("here");  
            var mySigLabel = document.getElementById('<%= SigDiv.ClientID %>');
            mySigLabel.style.display = null;
            var myButton = document.getElementById('<%= btnAddClientVisit.ClientID %>');
            myButton.disabled = true;
            myButton.style.display = "none";
            
            $("#SignBtn").css("display", "none");
            $("#ClearBtn").css("display", "");
            $("#DoneBtn").css("display", "");
           
            


            var ctx = document.getElementById("cnv").getContext("2d");
            SetDisplayXSize(500);
            SetDisplayYSize(100);
            SetJustifyMode(0);
            // SetEncryptionMode(0);
            SetTabletState(0, tmr);
            SetJustifyMode(0);
            ClearTablet();
            tmr = SetTabletState(1, ctx, 50) || tmr;
        } else {
            alert("Unable to communicate with SigWeb. Please confirm that SigWeb is installed and running on this PC.");
        }

    }

    function onClear() {
        ClearTablet();
    }

    function onDone() {
        if (NumberOfTabletPoints() == 0) {
            alert("Please sign before continuing");
        }
        else {
            SetTabletState(0, tmr);
            //RETURN TOPAZ-FORMAT SIGSTRING
            SetSigCompressionMode(1);
            var elem = document.getElementById('<%= HiddenFieldImgData.ClientID %>');
            elem.value = GetSigString();
            SetImageXSize(500);
            SetImageYSize(100);
            SetImagePenWidth(5);
            GetSigImageB64(SigImageCallback);
            //   var SIGSTRINGIMAGE = GetSigImageB64(SigImageCallback);


            //   alert("Signature created. Submit Form to Save.");

        }
    }

    function SigImageCallback(str) {

        document.getElementById('<%= HiddenFieldSignData.ClientID %>').value = str;
      //    document.getElementById("<%=btnAddClientVisit.ClientID%>").disabled = false;
        var myButton = document.getElementById('<%= btnAddClientVisit.ClientID %>');
        $(myButton).removeAttr('disabled');
        $(myButton).click();
        //   alert("Signature created. Please submit form to save.");

    }

    window.onunload = window.onbeforeunload = (function () {
        closingSigWeb();
    })

    function closingSigWeb() {
        ClearTablet();
        SetTabletState(0, tmr);
    }

    function RunOnDone() {
        // put your code here 
        onDone();
        return false;
    }

 
    // END SIGNATURE JAVASCRIPT
    // END SIGNATURE JAVASCRIPT
    // END SIGNATURE JAVASCRIPT


    // DYMO JAVASCRIPT
    // DYMO JAVASCRIPT
    // DYMO JAVASCRIPT

    function updatePreview() {
        if (!label)
            return;

        var pngData = label.render();
        var labelImage = document.getElementById('labelImage');
        labelImage.src = "data:image/png;base64," + pngData;
    }

    $(function () {
        $("#<%= printDivLabel.ClientID %>").click(function () {

            var line1 = $("#<%= hidLabelContent.ClientID %>").val();
            var line2 = $("#<%= hidLabelContent2.ClientID %>").val();
            var line3 = $("#<%= hidLabelContent3.ClientID %>").val();
            var xmlLabel = $("#<%= hidLabelXML.ClientID %>").val();
            var quantityPrint = $("#<%= hidPrintShoppingLabelQuantity.ClientID %>").val();
            //async function printLabel(line1, line2, line3, howmanycopies, xmlLabel)
            printLabel(line1, line2, line3, quantityPrint, xmlLabel);
        }
        );

        $("#<%= printDivBarCodeLabel.ClientID %>").click(function () {

            var line1 = $("#<%= hidLabelContent.ClientID %>").val();
            var barcodeNumber = $("#<%= hidLabelContentBarCode.ClientID %>").val();
            var xmlLabelBarCode = $("#<%= hidLabelBarCode.ClientID %>").val();

            //   async function printBarCodeLabel(line1, barcodenumber, howmanycopies, xmlLabel)
            printBarCodeLabel(line1, barcodeNumber, 1, xmlLabelBarCode);

        }
        );

    });

    // END DYMO JAVASCRIPT
    // END DYMO JAVASCRIPT
    // END DYMO JAVASCRIPT


</script>

<asp:HiddenField ID="hidLabelContent" runat="server"  />
<asp:HiddenField ID="hidLabelContent2" runat="server"  />
<asp:HiddenField ID="hidLabelContent3" runat="server"  />
<asp:HiddenField ID="hidLabelContentBarCode" runat="server"  />
<asp:HiddenField ID="hidPrintShoppingLabelQuantity" runat="server"  />
<asp:HiddenField ID="hidLabelXML" runat="server" Value='<?xml version="1.0" encoding="utf-8"?><DesktopLabel Version="1">  <DYMOLabel Version="3"><Description>DYMO Label</Description><Orientation>Landscape</Orientation><LabelName>Address</LabelName><InitialLength>0</InitialLength><BorderStyle>SolidLine</BorderStyle><DYMORect><DYMOPoint><X>0.23</X><Y>0.06</Y></DYMOPoint><Size><Width>3.21</Width><Height>0.9966666</Height></Size></DYMORect><BorderColor><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></BorderColor><BorderThickness>1</BorderThickness><Show_Border>False</Show_Border><DynamicLayoutManager><RotationBehavior>ClearObjects</RotationBehavior><LabelObjects><TextObject><Name>Line_1</Name><Brushes><BackgroundBrush><SolidColorBrush><Color A="0" R="1" G="1" B="1"></Color></SolidColorBrush></BackgroundBrush><BorderBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></BorderBrush><StrokeBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></StrokeBrush><FillBrush><SolidColorBrush>  <Color A="0" R="1" G="1" B="1"></Color></SolidColorBrush></FillBrush></Brushes><Rotation>Rotation0</Rotation><OutlineThickness>1</OutlineThickness><IsOutlined>False</IsOutlined><BorderStyle>SolidLine</BorderStyle><Margin><DYMOThickness Left="0" Top="0" Right="0" Bottom="0" /></Margin><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><FitMode>AlwaysFit</FitMode><IsVertical>False</IsVertical><FormattedText><FitMode>AlwaysFit</FitMode><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><IsVertical>False</IsVertical><LineTextSpan><TextSpan>  <Text>Enter Text</Text>  <FontInfo><FontName>Arial</FontName><FontSize>23.4</FontSize><IsBold>False</IsBold><IsItalic>False</IsItalic><IsUnderline>False</IsUnderline><FontBrush>  <SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color>  </SolidColorBrush></FontBrush>  </FontInfo></TextSpan></LineTextSpan></FormattedText><ObjectLayout><DYMOPoint><X>0.3867604</X><Y>0.0716153</Y></DYMOPoint><Size><Width>2.726479</Width><Height>0.3758107</Height></Size></ObjectLayout></TextObject><TextObject><Name>Line_2</Name><Brushes><BackgroundBrush><SolidColorBrush>  <Color A="0" R="0" G="0" B="0"></Color></SolidColorBrush></BackgroundBrush><BorderBrush><SolidColorBrush>  <Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></BorderBrush><StrokeBrush><SolidColorBrush>  <Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></StrokeBrush><FillBrush><SolidColorBrush>  <Color A="0" R="0" G="0" B="0"></Color></SolidColorBrush></FillBrush></Brushes><Rotation>Rotation0</Rotation><OutlineThickness>1</OutlineThickness><IsOutlined>False</IsOutlined><BorderStyle>SolidLine</BorderStyle><Margin><DYMOThickness Left="0" Top="0" Right="0" Bottom="0" /></Margin><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><FitMode>AlwaysFit</FitMode><IsVertical>False</IsVertical><FormattedText><FitMode>AlwaysFit</FitMode><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><IsVertical>False</IsVertical><LineTextSpan><TextSpan>  <Text>ABC</Text>  <FontInfo><FontName>Segoe UI</FontName><FontSize>19.3</FontSize><IsBold>False</IsBold><IsItalic>False</IsItalic><IsUnderline>False</IsUnderline><FontBrush>  <SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color>  </SolidColorBrush></FontBrush>  </FontInfo></TextSpan></LineTextSpan></FormattedText><ObjectLayout><DYMOPoint><X>0.2437118</X><Y>0.3820833</Y></DYMOPoint><Size><Width>3.046935</Width><Height>0.3600001</Height></Size></ObjectLayout></TextObject><TextObject><Name>Line_3</Name><Brushes><BackgroundBrush><SolidColorBrush>  <Color A="0" R="0" G="0" B="0"></Color></SolidColorBrush></BackgroundBrush><BorderBrush><SolidColorBrush>  <Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></BorderBrush><StrokeBrush><SolidColorBrush>  <Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></StrokeBrush><FillBrush><SolidColorBrush>  <Color A="0" R="0" G="0" B="0"></Color></SolidColorBrush></FillBrush></Brushes><Rotation>Rotation0</Rotation><OutlineThickness>1</OutlineThickness><IsOutlined>False</IsOutlined><BorderStyle>SolidLine</BorderStyle><Margin><DYMOThickness Left="0" Top="0" Right="0" Bottom="0" /></Margin><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><FitMode>AlwaysFit</FitMode><IsVertical>False</IsVertical><FormattedText><FitMode>AlwaysFit</FitMode><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><IsVertical>False</IsVertical><LineTextSpan><TextSpan>  <Text>ABC</Text>  <FontInfo><FontName>Segoe UI</FontName><FontSize>21.2</FontSize><IsBold>False</IsBold><IsItalic>False</IsItalic><IsUnderline>False</IsUnderline><FontBrush>  <SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color>  </SolidColorBrush></FontBrush>  </FontInfo></TextSpan></LineTextSpan></FormattedText><ObjectLayout><DYMOPoint><X>0.5063462</X><Y>0.6558337</Y></DYMOPoint><Size><Width>2.438336</Width><Height>0.3979165</Height></Size></ObjectLayout></TextObject></LabelObjects></DynamicLayoutManager></DYMOLabel><LabelApplication>Blank</LabelApplication><DataTable><Columns></Columns><Rows></Rows></DataTable></DesktopLabel>' />
<asp:HiddenField ID="hidLabelBarCode" runat="server" Value='<?xml version="1.0" encoding="utf-8"?><DesktopLabel Version="1"><DYMOLabel Version="3"><Description>DYMO Label</Description><Orientation>Landscape</Orientation><LabelName>Address</LabelName><InitialLength>0</InitialLength><BorderStyle>SolidLine</BorderStyle><DYMORect><DYMOPoint><X>0.23</X><Y>0.06</Y></DYMOPoint><Size><Width>3.21</Width><Height>0.9966666</Height></Size></DYMORect><BorderColor><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></BorderColor><BorderThickness>1</BorderThickness><Show_Border>False</Show_Border><DynamicLayoutManager><RotationBehavior>ClearObjects</RotationBehavior><LabelObjects><TextObject><Name>TextObject0</Name><Brushes><BackgroundBrush><SolidColorBrush><Color A="0" R="0" G="0" B="0"></Color></SolidColorBrush></BackgroundBrush><BorderBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></BorderBrush><StrokeBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></StrokeBrush><FillBrush><SolidColorBrush><Color A="0" R="0" G="0" B="0"></Color></SolidColorBrush></FillBrush></Brushes><Rotation>Rotation0</Rotation><OutlineThickness>1</OutlineThickness><IsOutlined>False</IsOutlined><BorderStyle>SolidLine</BorderStyle><Margin><DYMOThickness Left="0" Top="0" Right="0" Bottom="0" /></Margin><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><FitMode>AlwaysFit</FitMode><IsVertical>False</IsVertical><FormattedText><FitMode>AlwaysFit</FitMode><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><IsVertical>False</IsVertical><LineTextSpan><TextSpan><Text>ABC</Text><FontInfo><FontName>Segoe UI</FontName><FontSize>25.1</FontSize><IsBold>False</IsBold><IsItalic>False</IsItalic><IsUnderline>False</IsUnderline><FontBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></FontBrush></FontInfo></TextSpan></LineTextSpan></FormattedText><ObjectLayout><DYMOPoint><X>0.25</X><Y>0.06041637</Y></DYMOPoint><Size><Width>3.006255</Width><Height>0.4683344</Height></Size></ObjectLayout></TextObject><BarcodeObject><Name>BarcodeObject0</Name><Brushes><BackgroundBrush><SolidColorBrush><Color A="1" R="1" G="1" B="1"></Color></SolidColorBrush></BackgroundBrush><BorderBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></BorderBrush><StrokeBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></StrokeBrush><FillBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></FillBrush></Brushes><Rotation>Rotation0</Rotation><OutlineThickness>1</OutlineThickness><IsOutlined>False</IsOutlined><BorderStyle>SolidLine</BorderStyle><Margin><DYMOThickness Left="0" Top="0" Right="0" Bottom="0" /></Margin><BarcodeFormat>Code128Auto</BarcodeFormat><Data><DataString>123</DataString></Data><HorizontalAlignment>Center</HorizontalAlignment><VerticalAlignment>Middle</VerticalAlignment><Size>SmallMedium</Size><TextPosition>Bottom</TextPosition><FontInfo><FontName>Arial</FontName><FontSize>12</FontSize><IsBold>False</IsBold><IsItalic>False</IsItalic><IsUnderline>False</IsUnderline><FontBrush><SolidColorBrush><Color A="1" R="0" G="0" B="0"></Color></SolidColorBrush></FontBrush></FontInfo><ObjectLayout><DYMOPoint><X>0.2595834</X><Y>0.5287508</Y></DYMOPoint><Size><Width>3.03293</Width><Height>0.527916</Height></Size></ObjectLayout></BarcodeObject></LabelObjects></DynamicLayoutManager></DYMOLabel><LabelApplication>Blank</LabelApplication><DataTable><Columns></Columns><Rows></Rows></DataTable></DesktopLabel>' />
<div style="clear:both;"></div>

    <div class="row">
        <div class="col-sm-5 ClientNameHeader">
             <div> <asp:HyperLink ID="HyperLinkPhotoID" runat="server" Visible="false">Manage PhotoID</asp:HyperLink></div>
            <asp:Label ID="lblClientNameHeader" runat="server" Text="" />
          
        </div>
        <div class="col-sm-2">
            
            <asp:Image ID="ImageIDClient" runat="server" Height="100" CssClass="hover-zoom" /> 
            &nbsp;
        </div>
        <div class="col-sm-5 ClientNameHeaderRight"><asp:Label ID="lblClientNameHeaderRight" runat="server" Text=""></asp:Label><br /><span style="font-size: 11px;"><asp:Repeater ID="rp_AgeGroupReport" runat="server">
<ItemTemplate><%# DataBinder.Eval(Container.DataItem, "AgeGroupCount")%> <%# DataBinder.Eval(Container.DataItem, "AgeGroup") %></ItemTemplate>
<SeparatorTemplate>, </SeparatorTemplate> 
</asp:Repeater></span>
<br /><asp:Label ID="lblBudget" runat="server" Text="" /></div>
    </div>

<div class="ClientNameHeaderRight">

    <div id="printDivLabel" class="btn btn-default" runat="server" ClientIDMode="Static" >Print Shopping Label</div> 
    <div id="printDivBarCodeLabel" class="btn btn-default" runat="server" ClientIDMode="Static" >Print Barcode Label</div> 
  
        
</div>

<div class="ClientNameHeader">
    


</div>


<div class="ClientNotesQuickView"><asp:Image
        ID="ImgFlagged" runat="server" ImageUrl="~/DesktopModules/GIBS/FBClients/Images/flag.png" Visible="false" ImageAlign="AbsMiddle" />
        <asp:Label ID="lblClientNoteQuickView" runat="server" Text=""></asp:Label>
</div>






<div style="text-align: center;" class="alert alert-success" id="ErrorMessage" runat="server"><asp:Label ID="lblMessage" runat="server" Text="" CssClass="Normal"></asp:Label></div>



  <div class="dnnForm" id="tabs-client">
        <ul class="dnnAdminTabNav">
            <li><a href="#Client">Client</a></li>
            <li><a href="#AFM">Household Members</a></li>
            <li><a href="#IncomeExpense"><asp:Label ID="lblIncomeExpense" runat="server" Text="Income & Expense" /></a></li>
            <li><a href="#Map">Map</a></li>
            <li><a href="#Visits">Client Visits</a></li>
        </ul>

<!---- BEGIN Client TAB ----->
        <div id="Client" class="dnnClear">
            


 <div style="-webkit-transform: translateZ(0); float:right;padding-right:5px;"><asp:linkbutton id="cmdUpdate" resourcekey="cmdUpdate" ValidationGroup="UserForm" 
        runat="server" text="Update" OnClick="cmdUpdate_Click" OnClientClick="UseData();dataChanged=0;" CssClass="btn btn-primary"></asp:linkbutton></div>  

    <ul class="dnnActions dnnClear" style="text-align:right;">
      
        <li><asp:linkbutton id="cmdDelete" resourcekey="cmdDelete"  
        runat="server" text="Delete" OnClick="cmdDelete_Click" OnClientClick="UseData();" CssClass="btn btn-primary"></asp:linkbutton></li>
      <li>  <asp:LinkButton ID="LinkButtonMerge" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to MERGE another client this record?');"     
             CommandArgument='<%# Eval("IeID") %>' Text="Merge" CssClass="btn btn-default"
             CommandName="Merge" runat="server" onclick="LinkButtonMerge_Click" Visible="false">
             </asp:LinkButton></li>
        
    </ul> 


<div class="dnnForm" id="form-client-record">

    <fieldset>

<dnn:sectionhead id="sectGeneralSettings" cssclass="Head" runat="server" text="Client Name & Demographics" section="GeneralSection"
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="GeneralSection" runat="server">   

        <div class="dnnFormItem">
    
	            <dnn:label id="lblClientStatus" runat="server" resourcekey="lblClientStatus" controlname="ddlClientStatus" suffix=":" />
		            <asp:DropDownList ID="ddlClientStatus" runat="server">
                            <asp:ListItem Text="Active" Value="True"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="False"></asp:ListItem>
				            </asp:DropDownList>

		</div>

        <div class="dnnFormItem">
             <asp:Panel ID="PanelClientType" runat="server">
    
	            <dnn:label id="lblClientType" runat="server" resourcekey="lblClientType" controlname="ddlClientType" suffix=":" />
		            <asp:DropDownList ID="ddlClientType" runat="server" TabIndex="1" OnSelectedIndexChanged="clientType_SelectedIndexChanged" AutoPostBack="True">
		
                            <asp:ListItem Text="Individual" Value="Individual"></asp:ListItem>
                            <asp:ListItem Text="Group Home" Value="Group Home"></asp:ListItem>
                        <asp:ListItem Text="Pallet" Value="Pallet"></asp:ListItem>
                        

				            </asp:DropDownList>
            </asp:Panel>
		</div>

    		<div id="divClientServiceLocation" runat="server" class="dnnFormItem">						
					 <dnn:Label runat="server" ID="lblClientServiceLocation" ControlName="ddlClientServiceLocation" ResourceKey="lblClientServiceLocation" Suffix=":" /> 
 <asp:DropDownList ID="ddlClientServiceLocation" runat="server" EnableViewState="true" /><asp:RequiredFieldValidator runat="server" id="reqClientOrigination"  
            controltovalidate="ddlClientServiceLocation" InitialValue="" CssClass="dnnFormMessage dnnFormError" ResourceKey="ClientOriginationError" ValidationGroup="UserForm" /> 
		</div>		

		<div class="dnnFormItem">
<dnn:label id="lblClientFirstMiddleLastName" runat="server" controlname="txtClientFirstName" suffix=":" ResourceKey="lblClientFirstName" />
<asp:TextBox ID="txtClientFirstName" runat="server" ValidationGroup="UserForm" CssClass="dnnFormRequired" TabIndex="2" placeholder="First Name" MaxLength="30" AutoCompleteType="Disabled" />
<asp:RequiredFieldValidator runat="server" id="reqClientFirstName" resourcekey="reqClientFirstName" controltovalidate="txtClientFirstName" CssClass="dnnFormMessage dnnFormError" errormessage="Required!" ValidationGroup="UserForm" />
        </div>

		<div class="dnnFormItem">
			<dnn:label id="lblClientMiddleName" runat="server" controlname="txtClientMiddleInitial" suffix=":" ResourceKey="lblClientMiddleName" />
            <asp:TextBox ID="txtClientMiddleInitial" runat="server" ValidationGroup="UserForm" TabIndex="3" MaxLength="30" AutoCompleteType="Disabled" />
        </div>
        <div class="dnnFormItem">
			<dnn:label id="lblClientLastName" runat="server" controlname="txtClientLastName" suffix=":" Text="Last Name" />
            <asp:TextBox ID="txtClientLastName" runat="server" ValidationGroup="UserForm" CssClass="dnnFormRequired" TabIndex="4" MaxLength="30" AutoCompleteType="Disabled" />
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" resourcekey="reqClientLastName" controltovalidate="txtClientLastName" CssClass="dnnFormMessage dnnFormError" errormessage="Required!" ValidationGroup="UserForm" />
        </div>
       <asp:Panel ID="PanelShowSuffix" runat="server">
		<div class="dnnFormItem">
			<dnn:label id="lblClientSuffix" runat="server" controlname="txtClientFirstName" suffix=":" ResourceKey="lblClientSuffix" />
            <asp:DropDownList ID="ddlClientSuffix" runat="server" TabIndex="5"></asp:DropDownList>
		</div>	
        </asp:Panel>
        <div class="dnnFormItem">
            <dnn:label id="lblClientDOB" runat="server" resourcekey="lblClientDOB" controlname="txtClientDOB" suffix=":" />
            <asp:TextBox ID="txtClientDOB" runat="server" ClientIDMode="Static" TabIndex="6" AutoCompleteType="Disabled" />
            <asp:RangeValidator ID="Range1" ControlToValidate="txtClientDOB" 
Type="Date" Format="MM/DD/YYYY" ValidationGroup="UserForm" 
CssClass="dnnFormMessage dnnFormError" 
resourcekey="reqClientDOBRange" Display="Dynamic"
runat="server" />            
            <asp:RequiredFieldValidator runat="server" id="reqClientDOB" controltovalidate="txtClientDOB" resourcekey="reqClientDOB" 
            CssClass="dnnFormMessage dnnFormError" ValidationGroup="UserForm" />

        </div>
		<div class="dnnFormItem">
            <dnn:label id="lblClientGender" runat="server" resourcekey="lblClientGender" controlname="ddlClientGender" suffix=":" />
            <asp:DropDownList ID="ddlClientGender" runat="server" TabIndex="7">
		        <asp:ListItem Text="----" Value=""></asp:ListItem>
                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                <asp:ListItem Value="Non-Label" Text="Non-Labeling"></asp:ListItem>
                <asp:ListItem Value="NoAnswer" Text="Prefers Not To Answer"></asp:ListItem>
			</asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqClientGender" runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="ddlClientGender" resourcekey="reqClientGender" Enabled="false" InitialValue=""  ValidationGroup="UserForm" />
		</div>		

        <div class="dnnFormItem">
            <dnn:label id="lblClientEthnicity" runat="server" resourcekey="lblClientEthnicity" controlname="ddlClientEthnicity" suffix=":" />
            <asp:DropDownList ID="ddlClientEthnicity" runat="server" TabIndex="8" ></asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqClientEthnicity" runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="ddlClientEthnicity" InitialValue="" Enabled="false" resourcekey="reqClientEthnicity" ValidationGroup="UserForm" />
        </div>


<div class="dnnFormItem">
            <dnn:label id="lblClientLanguage" runat="server" resourcekey="lblClientLanguage" controlname="ddlClientLanguage" suffix=":" />
            <asp:DropDownList ID="ddlClientLanguage" runat="server" TabIndex="9" ></asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqClientLanguage" runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="ddlClientLanguage" InitialValue="" Enabled="false" resourcekey="reqClientLanguage" ValidationGroup="UserForm" />
        </div>


		<div class="dnnFormItem">
            <dnn:label id="lblVerifyDate" runat="server" resourcekey="lblVerifyDate" controlname="txtVerifyDate" suffix=":" />
<asp:TextBox ID="txtVerifyDate" runat="server" ClientIDMode="Static" Width="120px" TabIndex="10" AutoCompleteType="Disabled" /> 
                    <asp:CheckBox ID="cbxDOB_Verified" runat="server" resourcekey="cbxDOB_Verified" OnClick="requireClientVerifyDate();" TabIndex="11" />
 <asp:RequiredFieldValidator ID="rfvClientVerifyDate" ControlToValidate="txtVerifyDate" ForeColor="Red" ErrorMessage="<b>You must enter a date.</b>" runat="server" Display="Dynamic" ValidationGroup="UserForm" Enabled="false" />                  
		</div>		
        

</div>





<dnn:sectionhead id="sectAddressHeader" cssclass="Head" runat="server" text="Address, E-Mail & Phone" section="sectAddress"
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="sectAddress" runat="server">   

        <div class="dnnFormItem">
            <dnn:label id="lblAddress" runat="server" resourcekey="lblAddress" controlname="txtAddress" suffix=":" />
		    <asp:TextBox ID="txtAddress" runat="server" ValidationGroup="UserForm" MaxLength="50" TabIndex="12" AutoCompleteType="Disabled" />
		    <asp:RequiredFieldValidator runat="server" id="reqAddress" resourcekey="reqAddress" controltovalidate="txtAddress" errormessage="Required!-----" CssClass="dnnFormMessage dnnFormError" ValidationGroup="UserForm" />

        </div>
		<div class="dnnFormItem">
            <dnn:label id="lblUnit" runat="server" resourcekey="lblUnit" controlname="txtUnit" suffix=":"/>
		    <asp:TextBox ID="txtUnit" runat="server" TabIndex="13" AutoCompleteType="Disabled" />
		</div>		
        
        <div class="dnnFormItem">
            <dnn:label id="lblCityStateZip" runat="server" resourcekey="lblCityStateZip" controlname="ddlCity" suffix=":" />
		    <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" TabIndex="14"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" id="reqCity" resourcekey="reqCity" controltovalidate="ddlCity" InitialValue="-1" CssClass="dnnFormMessage dnnFormError" ValidationGroup="UserForm" />
        </div>

		<div class="dnnFormItem">
			<dnn:label id="lblClientVillage" runat="server" controlname="ddlTown" suffix=":" ResourceKey="lblClientVillage" />
			<asp:DropDownList ID="ddlTown" runat="server" TabIndex="15"></asp:DropDownList>
            <asp:TextBox ID="txtOtherTown" Visible="False" runat="server" />
            <asp:RequiredFieldValidator runat="server" id="reqTown" Enabled="False" resourcekey="reqTown" controltovalidate="ddlTown" InitialValue="-1" errormessage="Required!" CssClass="dnnFormMessage dnnFormError" ValidationGroup="UserForm" />
        </div>

		<div class="dnnFormItem">
            <dnn:label id="lblClientState" runat="server" controlname="ddlState" suffix=":" ResourceKey="lblClientState" />
            <asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList>
		</div>	
        	
        <div class="dnnFormItem">
			<dnn:label id="lblClientZipCode" runat="server" controlname="txtZip" suffix=":" ResourceKey="lblClientZipCode" />
			<asp:TextBox ID="txtZip" runat="server" Width="200px" TabIndex="16" AutoCompleteType="Disabled" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="lblClientAddressVerify" runat="server" resourcekey="lblClientAddressVerify" controlname="ClientAddressVerifyDate" suffix=":" />
            <asp:TextBox ID="txtClientAddressVerifyDate" runat="server" ClientIDMode="Static" Width="120px" TabIndex="17" />
            <asp:CheckBox ID="cbxClientAddressVerify" runat="server" resourcekey="cbxClientAddressVerify" OnClick="requireAddressVerifyDate();" TabIndex="18" />
            <asp:RequiredFieldValidator ID="rfvAddressVerifyDate" ControlToValidate="txtClientAddressVerifyDate" ForeColor="Red" ErrorMessage="<b>You must enter a date.</b>" runat="server" Display="Dynamic" ValidationGroup="UserForm" Enabled="false" />      
        </div>
<asp:Panel ID="PanelShowOneBagOnly" runat="server">		
		<div class="dnnFormItem">
	        <dnn:label id="lblOneBagOnly" runat="server" resourcekey="lblOneBagOnly" controlname="cbxOneBagOnly" suffix=":"></dnn:label>
		    <asp:CheckBox ID="cbxOneBagOnly" runat="server" resourcekey="cbxOneBagOnly"  Font-Size="12px" />
		</div>	
</asp:Panel>

        <div class="dnnFormItem">
            <dnn:label id="lblEmail" runat="server" controlname="txtEmail" suffix=":" />
            <asp:TextBox ID="txtEmail" runat="server" TabIndex="19" AutoCompleteType="Disabled" />
             <asp:RequiredFieldValidator ID="rfvtxtEmail" ControlToValidate="txtEmail" CssClass="dnnFormMessage dnnFormError" ErrorMessage="E-Mail Address is Required!" runat="server"
                 Display="Dynamic" ValidationGroup="UserForm" Enabled="false" /> 
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="txtEmail"
    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
    Display = "Dynamic" CssClass="dnnFormMessage dnnFormError" ErrorMessage = "Invalid E-Mail Address!"/>
        </div>
		<div class="dnnFormItem">
            <dnn:label id="lblPhone" resourcekey="lblPhone" runat="server" controlname="txtPhone" suffix=":" />
		    <asp:TextBox ID="txtPhone" runat="server" Width="200px" TabIndex="20" AutoCompleteType="Disabled" /> <asp:DropDownList 
                ID="ddlClientPhoneType" runat="server" Width="100px" TabIndex="21">
                <asp:ListItem Text="-- Type --" Value="0"></asp:ListItem>
                <asp:ListItem Text="Home"></asp:ListItem>
                <asp:ListItem Text="Cell"></asp:ListItem>
                <asp:ListItem Text="Work"></asp:ListItem>
                </asp:DropDownList>
		</div>	


</div>



<dnn:sectionhead id="sectOtherHeader" cssclass="Head" runat="server" text="Other" section="sectOther" 
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="sectOther" runat="server">   

    <div class="dnnFormItem">
        <dnn:label id="lblClientQuestions" runat="server" controlname="cblClientTrueFalseQuestions" suffix=":" ResourceKey="lblClientQuestions" />
        
    <asp:CheckBoxList ID="cblClientTrueFalseQuestions" runat="server" 
           RepeatLayout="UnorderedList" CssClass="myGroupCheckBox" >
    </asp:CheckBoxList>
    
        </div>

        <div class="dnnFormItem">
            <dnn:label id="lblCaseWorker" runat="server" resourcekey="lblCaseWorker" controlname="ddlCaseWorker" suffix=":" />
            <asp:DropDownList ID="ddlCaseWorker" runat="server" TabIndex="22"></asp:DropDownList>
        </div>

    <div class="dnnFormItem" id="DisabilitiesRow" runat="server">
        <dnn:label id="lblClientDisability" runat="server" controlname="cblClientDisability" suffix=":" ResourceKey="lblClientDisability" />
        
    <asp:CheckBoxList ID="cblClientDisability" runat="server" 
           RepeatLayout="UnorderedList" CssClass="myGroupCheckBox" >
    </asp:CheckBoxList>
    
    </div>

		<div class="dnnFormItem">
            <dnn:label id="lblSubjectToReview" runat="server" resourcekey="lblSubjectToReview" controlname="cbxSubjectToReview" suffix=":" />
		    <asp:CheckBox ID="cbxSubjectToReview" runat="server" resourcekey="cbxSubjectToReview"  Font-Size="12px" />
		</div>	
        <div class="dnnFormItem">
            <dnn:label id="lblClientNote" runat="server" resourcekey="lblClientNote" controlname="txtClientNote" suffix=":" />
		    <asp:TextBox ID="txtClientNote" runat="server" Width="100%" Height="120px" TextMode="MultiLine" TabIndex="23" />
        </div>
        	
        <div class="dnnFormItem">
            <dnn:label id="lblClientIdCard" runat="server" resourcekey="lblClientIdCard" controlname="txtClientIdCard" suffix=":" />
		    <asp:TextBox ID="txtClientIdCard" runat="server" />
        </div>		

       <div class="dnnFormItem">
            <dnn:label id="lblIsLocked" runat="server" resourcekey="lblIsLocked" controlname="cbxIsLocked" suffix=":" />
		    <asp:CheckBox ID="cbxIsLocked" runat="server" />
        </div>	

        <div class="dnnFormItem">
            <dnn:label id="lblLastUpdate" runat="server" resourcekey="lblLastUpdate" suffix=":" />
		    <asp:Label ID="txtLastUpdatedBy" runat="server" Text="" CssClass="dnnFormInput" /> 
		</div>


</div>
<asp:HiddenField ID="hidClientID" runat="server" Value="" />  
    </fieldset>
</div>	



 


        </div>
<!---- END Client TAB ----->




<!---- BEGIN Photo TAB ----->
<!---- END Photo TAB ----->


<!---- BEGIN Map TAB ----->
 <div id="Map" class="dnnClear">
 

  <div style="position:relative;float:right;padding-right:30px;"><asp:LinkButton runat="server" ID="btnSaveMap" 
                CssClass="btn btn-primary" ResourceKey="btnSaveMap" ValidationGroup="UserForm" OnClientClick="UseData();dataChanged=0;"  
                onclick="btnSaveMap_Click" /></div>

	    <table id="tblLocationDetail" summary="Location Detail Table" border="0">
		<tr>
			<td class="SubHead"><dnn:label id="FindAddress" runat="server" controlname="txtLocAddress" ResourceKey="FindAddress" suffix=":"></dnn:label></td>
			<td>
			    <asp:TextBox ID="txtLocAddress" runat="server" onkeydown="if(event.keyCode==13) {showAddress(this.value);}" Width="240px" />
			    <input type="button" value="Find Address!" id="findaddressbutton" />
			</td>
		</tr>
		<tr>
			<td class="SubHead"><dnn:label id="lblLatitude" runat="server" ResourceKey="lblLatitude" controlname="txtLatitude" suffix=":"></dnn:label></td>
			<td>
			    <asp:TextBox ID="txtLatitude" runat="server" Width="100px" ClientIDMode="Static" Text="" />
			</td>
		</tr>
		<tr>
			<td class="SubHead"><dnn:label id="lblLongitude" runat="server" ResourceKey="lblLongitude" controlname="txtLongitude" suffix=":"></dnn:label></td>
			<td>
			    <asp:TextBox ID="txtLongitude" runat="server" Width="100px" ClientIDMode="Static" Text="" />
			</td>
		</tr>

		</table>

                <div id="MapDiv" style="width: 100%; height: 500px; border:1px solid black; margin: 0 auto;"></div>


<script async defer src="<%= GetMapUrl() %>" type="text/javascript"></script>                		    
                <script type="text/javascript">

                    function load() {
                        // The location of Uluru
                    //    var uluru = { lat: mycurrentlat, lng: mycurrentlong };
                        <asp:literal id="litMapCenter" runat="server" />
                        // The map, centered at Uluru
                        // mapTypeId: 'satellite'
                        var map = new google.maps.Map(
                            document.getElementById('MapDiv'), { zoom: 18, center: uluru, mapTypeId: 'roadmap' });

                        // The marker, positioned at Uluru
                        var marker = new google.maps.Marker({
                            position: uluru,
                            map: map,
                            title: 'Client Address',
                            draggable: true
                        });


                        var geocoder = new google.maps.Geocoder();

                        google.maps.event.addListener(marker, 'dragend', function (marker) {
                            var latLng = marker.latLng;
                       
                            document.getElementById("<%= txtLatitude.ClientID %>").value = latLng.lat();
                            document.getElementById("<%= txtLongitude.ClientID %>").value = latLng.lng();


                        });

                        

                        document.getElementById('findaddressbutton').addEventListener('click', function () {
                            geocodeAddress(geocoder, map, marker);
                        });

                    }

                    function geocodeAddress(geocoder, resultsMap, marker) {
                        var address = document.getElementById('<%= txtLocAddress.ClientID %>').value;
                        geocoder.geocode({ 'address': address }, function (results, status) {
                            if (status === 'OK') {
                                resultsMap.setCenter(results[0].geometry.location);

                                var latitude = results[0].geometry.location.lat();
                                var longitude = results[0].geometry.location.lng();

                                document.getElementById("<%= txtLatitude.ClientID %>").value = latitude;
                                document.getElementById("<%= txtLongitude.ClientID %>").value = longitude;
                               // alert(latitude);
                                marker.setPosition(results[0].geometry.location);
                                map.panTo(new google.maps.LatLng(latitude, longitude));
                                
                               // var marker = new google.maps.Marker({map: resultsMap, position: results[0].geometry.location});
                            } else {
                                alert('Geocode was not successful for the following reason: ' + status);
                            }
                        });
                    }


                </script>

 <br/>&nbsp;
 </div>
<!---- END Map TAB ----->

<!---- BEGIN IncomeExpense TAB    style="display:none;"   ----->
        <div id="IncomeExpense" class="dnnClear">
   

 <div style="position:relative;float:right;padding-right:30px;"></div>   
                
                

<div class="row">
    <div class="col-md-2 col-md-offset-2 text-right"><b>Monthly Income:</b></div>
    <div class="col-md-2"><asp:DropDownList ID="ddlClientIncome" runat="server" CssClass="form-control" >
            </asp:DropDownList></div>
    <div class="col-md-2"><asp:TextBox runat="server" ID="txtIeAmount" CssClass="form-control" /></div>
    <div class="col-md-2"><asp:LinkButton runat="server" ID="btnAddClientIncome" 
                ResourceKey="btnAddClientIncome" CssClass="btn btn-primary" 
                onclick="btnAddClientIncome_Click" /></div>
</div>


        <div class="row" id="ExpenseRow" runat="server">
    <div class="col-md-2 col-md-offset-2 text-right"><b>Monthly Expense:</b>
    </div>
    <div class="col-md-2"><asp:DropDownList ID="ddlClientExpense" runat="server" CssClass="form-control" >
            </asp:DropDownList></div>
    <div class="col-md-2"><asp:TextBox runat="server" ID="txtIeExpenseAmount" CssClass="form-control" /></div>
    </div>


        
<script type="text/javascript" language="javascript" >
    $('.onlynumeric').keypress(function (event) {
        if (event.which < 46
    || event.which > 59) {
            event.preventDefault();
        } // prevent if not number/dot

        if (event.which == 46
    && $(this).val().indexOf('.') != -1) {
            event.preventDefault();
        } // prevent if already dot
    });
</script>        
                       
    <fieldset>


	
        <div class="dnnFormItem">
           <dnn:Label runat="server" ID="lblIeAmount" ControlName="txtIeAmount" ResourceKey="lblIeAmount" />
            
        </div>


    </fieldset>

    <asp:HiddenField ID="hidIeExpenseID" runat="server" Value="0" />
    <asp:HiddenField ID="hidIeID" runat="server" Value="0" />

    <div class="row">
    <div class="col-md-4 col-md-offset-2">   <b><asp:Label ID="lblTotalIncome" runat="server" Text=""></asp:Label></b>
        <asp:GridView ID="gvIncome" runat="server" OnRowDataBound="gvIncome_RowDataBound" OnRowCommand="gvIncome_RowCommand" OnRowDeleting="gvIncome_RowDeleting" OnRowEditing="gvIncome_RowEditing" 
    DataKeyNames="IeID"     
    AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-list"  
    resourcekey="gvIncome" >
    
<PagerStyle CssClass="pagination-ys" />  
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>

        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("IeID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonDelete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this record?');"     
             CommandArgument='<%# Eval("IeID") %>' 
             CommandName="Delete" runat="server"><asp:image ID="imgDelete" runat="server" imageurl="~/images/delete.gif" AlternateText="Delete Record" /></asp:LinkButton>
         </ItemTemplate>
      
       
       
       </asp:TemplateField>

        <asp:BoundField HeaderText="Income" DataField="IeDescription" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ></asp:BoundField>
       
        <asp:BoundField HeaderText="Amount" DataField="IeAmount" ItemStyle-VerticalAlign="Top" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right"></asp:BoundField>

    </Columns>
</asp:GridView>	
 </div>
    
    <div class="col-md-4"><b><asp:Label ID="lblTotalExpense" runat="server" Text=""></asp:Label></b>
        
        <asp:GridView ID="gvExpense" runat="server" OnRowDataBound="gvExpense_RowDataBound" OnRowCommand="gvExpense_RowCommand" OnRowDeleting="gvExpense_RowDeleting"  OnRowEditing="gvExpense_RowEditing"
    DataKeyNames="IeID"     
    AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-list"  
    resourcekey="gvExpense" >
    
<PagerStyle CssClass="pagination-ys" />   
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>
        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("IeID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>
        <asp:TemplateField HeaderText="" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonDelete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this record?');"     
             CommandArgument='<%# Eval("IeID") %>' 
             CommandName="Delete" runat="server"><asp:image ID="imgDelete" runat="server" imageurl="~/images/delete.gif" AlternateText="Delete Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

        <asp:BoundField HeaderText="Expense" DataField="IeDescription" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ></asp:BoundField>
        <asp:BoundField HeaderText="Amount" DataField="IeAmount" ItemStyle-VerticalAlign="Top" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right" ></asp:BoundField>

    </Columns>
</asp:GridView>	
 </div>

</div>


		
<h2 id="ChristopherColumbus" class="dnnFormSectionHead"><a href="#">Click Here for Income Eligibility Guidelines</a></h2>
        <fieldset class="dnnClear">
            <asp:Literal ID="LiteralIncomeEligibilityGuidelines" runat="server"></asp:Literal>
        </fieldset>




        </div>		
<!---- END IncomeExpense TAB ----->

<!---- BEGIN Additional Family Members TAB ----->		
        <div id="AFM" class="dnnClear">



<asp:GridView ID="gvAFM" runat="server"  
    DataKeyNames="ClAddFamMemID" OnRowEditing="gvAFM_RowEditing" OnPageIndexChanging="gvAFM_PageIndexChanging" OnRowDataBound="gvAFM_RowDataBound" 
    OnRowCommand="gvAFM_RowCommand" OnRowDeleting="gvAFM_RowDeleting"       
    AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-list"  
    resourcekey="gvAFM" AllowPaging="True" PageSize="10">
    
<PagerStyle CssClass="pagination-ys" />    
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>
        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Top" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("ClAddFamMemID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/images/edit.gif" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>


        <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Top" ItemStyle-Width="28px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonDelete" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this household member?');"      
             CommandArgument='<%# Eval("ClAddFamMemID") %>' 
             CommandName="Delete" runat="server"><asp:image ID="imgDelete" runat="server" imageurl="~/images/delete.gif" AlternateText="Delete Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

       
        <asp:TemplateField HeaderText="Toys" ItemStyle-VerticalAlign="Top" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonPresent" CausesValidation="False" 
            Visible='<%# (Convert.ToBoolean(Eval("QualifiedToys")))%>'   
            Enabled='<%# (Convert.ToBoolean(Eval("AFMDOBVerify")))%>'   
             CommandArgument='<%# Eval("ClAddFamMemID") %>' 
             CommandName="Christmas" runat="server"><asp:image ID="imgToys" runat="server" 
             imageurl='<%# (Eval("VerifiedToys").Equals(true) ? "~/DesktopModules/GIBS/FBClients/Images/present.gif" : "~/DesktopModules/GIBS/FBClients/Images/presentNO.gif")%>' 
             AlternateText="Christmas Toys" 
             ToolTip='<%# (Eval("VerifiedToys").Equals(true) ? "This child is already signed up for toys." : "This child is elegible for toys.") + (Eval("AFMDOBVerify").Equals(true) ? " Child is verified." : " This child is needs to be verified before signing up for toys.")%>' /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>



        <asp:BoundField HeaderText="First Name" DataField="ClAddFamMemFirstName" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
        <asp:BoundField HeaderText="MI" DataField="AFMMiddleInitial" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" Visible="false" ></asp:BoundField>
        <asp:BoundField HeaderText="Last Name" DataField="ClAddFamMemLastName" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
        <asp:BoundField HeaderText="Suffix" DataField="AFMSuffix" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" Visible="false" ></asp:BoundField>
		<asp:BoundField HeaderText="DOB" ItemStyle-VerticalAlign="Top" DataField="ClAddFamMemDOB" DataFormatString="{0:d}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
       <asp:BoundField HeaderText="AFMDOBVerify" DataField="AFMDOBVerify" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
        <asp:BoundField HeaderText="Age" DataField="AFM_Age" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
  
        <asp:BoundField HeaderText="Gender" DataField="AFMGender" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
 

<asp:TemplateField HeaderText="Ethnicity" >
            <ItemTemplate>
                <asp:Label ID="lblAFMEthnicity" runat="server" Text='<%# Eval("AFMEthnicity") %>' />
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Verified" ItemStyle-HorizontalAlign="Center" >
            <ItemTemplate>
            <asp:Image ID="Image1" runat="server" ImageUrl='<%# (Eval("AFMDOBVerify").Equals(true) ? "~/DesktopModules/GIBS/FBClients/Images/yes.png" : "~/DesktopModules/GIBS/FBClients/Images/no.png")%>' />
            </ItemTemplate>
        </asp:TemplateField>

         <asp:BoundField HeaderText="Relationship" DataField="AFMRelationship" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
        <asp:BoundField HeaderText="Last Update" DataField="LastModifiedOnDate" ItemStyle-VerticalAlign="Top" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
        <asp:BoundField HeaderText="Updated By" DataField="LastModifiedByUserName" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    </Columns>
</asp:GridView>	



            <asp:HyperLink ID="HyperLinkXmas" runat="server" Text="Print Toy Ticket" Visible="false" CssClass="btn btn-default" Target="_blank" />



 <div style="position:relative;float:right;padding-right:5px;"><asp:LinkButton runat="server" ID="btnAddAFM" 
                CssClass="btn btn-primary" ResourceKey="btnAddAFM" ValidationGroup="AFM" OnClientClick="dataChanged=0;" 
                onclick="btnAddAFM_Click" /></div>          
   
   
    <div class="dnnForm" id="formChristmas" runat="server" visible="false">

    <h3>
        <asp:Label ID="lblToySignup" runat="server" Text="Label"></asp:Label></h3>

    <fieldset>



		<div class="dnnFormItem">					
            <dnn:label id="lblxMasYear" runat="server" suffix=":" controlname="txtxMasYear" ResourceKey="lblxMasYear" />
			<asp:TextBox ID="txtxMasYear" runat="server" ReadOnly="true"></asp:TextBox>
           
            <asp:LinkButton ID="LinkButtonDeleteXMasRecord" runat="server" CssClass="btn btn-default btn-sm" onclick="btnDeleteAFMXmasRecord_Click">Delete Xmas Record</asp:LinkButton>
            </div>
         
         	
        

        <div class="dnnFormItem" id="XmasSizes" runat="server">
            <dnn:Label runat="server" ID="lblSizes" ControlName="txtSizes" ResourceKey="lblSizes" Suffix=":" />
            <asp:DropDownList ID="ddlSizes" runat="server">
            <asp:ListItem Text="-- Please Select Size --" Value="" />
            <asp:ListItem Text="Men's Small" Value="Men's Small" />
            <asp:ListItem Text="Men's Medium" Value="Men's Medium" />
            <asp:ListItem Text="Men's Large" Value="Men's Large" />
            <asp:ListItem Text="Men's Extra-Large" Value="Men's Extra-Large" />
            <asp:ListItem Text="Women's Small" Value="Women's Small" />
            <asp:ListItem Text="Women's Medium" Value="Women's Medium" />
            <asp:ListItem Text="Women's Large" Value="Women's Large" />
            <asp:ListItem Text="Women's Extra-Large" Value="Women's Extra-Large" />
            <asp:ListItem Text="N/A" Value="N/A" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorXmasSizes" runat="server" ValidationGroup="AFM" CssClass="dnnFormMessage dnnFormError"
            ControlToValidate="ddlSizes" InitialValue="" ResourceKey="ddlSizes.Required"></asp:RequiredFieldValidator>
        </div>		
        <div class="dnnFormItem" id="ShowXmasGiftFieldsSection" visible="false" runat="server">




	 <div class="dnnFormItem"> 
		
            <dnn:Label runat="server" ID="lblXmasGift1" ControlName="txtXmasGift1" ResourceKey="lblXmasGift1" Suffix=":" />
            <asp:TextBox ID="txtXmasGift1" runat="server" />
    </div>
        <div class="dnnFormItem"> 
         
		<dnn:Label runat="server" ID="lblXmasGift1Size" ControlName="txtXmasGift1Size" ResourceKey="lblXmasGift1Size" Suffix=":" />
            <asp:TextBox ID="txtXmasGift1Size" runat="server" />
		
		
	</div>	
	
	<div class="dnnFormItem">
		
            <dnn:Label runat="server" ID="lblXmasGift2" ControlName="txtXmasGift2" ResourceKey="lblXmasGift2" Suffix=":" />
            <asp:TextBox ID="txtXmasGift2" runat="server" />
        </div>	
        <div class="dnnFormItem">
		
		<dnn:Label runat="server" ID="lblXmasGift2Size" ControlName="txtXmasGift2Size" ResourceKey="lblXmasGift2Size" Suffix=":" />
            <asp:TextBox ID="txtXmasGift2Size" runat="server" />
		</div>
		
        <div class="dnnFormItem" style="display:none;">
		
		<dnn:Label runat="server" ID="lblXmasGiftRecordID" ControlName="txtXmasGiftRecordID" ResourceKey="lblXmasGiftRecordID" Suffix=":" />
            <asp:TextBox ID="txtXmasGiftRecordID" runat="server" />
		</div>
	        </div>		

        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblXmasNotes" ControlName="txtXmasNotes" ResourceKey="lblXmasNotes" Suffix=":" />
            <asp:TextBox ID="txtXmasNotes" runat="server" TextMode="MultiLine" />
        </div>	

        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblBikeRaffle" ControlName="cbxBikeRaffle" ResourceKey="lblBikeRaffle" Suffix=":" />
            <asp:CheckBox ID="cbxBikeRaffle" runat="server" />
           
        </div>	
    
        <div class="dnnFormItem" id="bikeAwardedDate" runat="server">
            <dnn:Label ID="lblBikeAwardedDate" runat="server" CssClass="dnnFormLabel" AssociatedControlID="txtBikeAwardedDate" Suffix=":" />
            <asp:TextBox ID="txtBikeAwardedDate" runat="server" ClientIDMode="Static" />
        </div>		

        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblReceivedToys" ControlName="txtReceivedToysDate" ResourceKey="lblReceivedToys" Suffix=":" />
            
            <asp:TextBox ID="txtReceivedToysDate" runat="server" ClientIDMode="Static" />
        </div>	
        <div class="dnnFormItem">
            <h5>
                <asp:Label ID="LabelXmasRecordID" runat="server" Text="RecordID"></asp:Label>
            </h5>
            </div>
        <asp:HiddenField ID="hidXmasID" runat="server" />


    </fieldset>

        <asp:GridView ID="GridViewXmasHistory" runat="server" DataKeyNames="xMasID" AutoGenerateColumns="false" OnRowDataBound="GridViewXmasHistory_OnRowDataBound" CssClass="table table-striped table-bordered table-list" >
        <Columns>
        <asp:BoundField HeaderText="Year" DataField="xMasYear" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
        
        <asp:BoundField HeaderText="Received Toys" DataField="ReceivedToysDate" DataFormatString="{0:d}" ItemStyle-VerticalAlign="Top" HtmlEncode="false" />
        <asp:BoundField HeaderText="Size" DataField="xMasSizes" ItemStyle-VerticalAlign="Top" Visible="false" ></asp:BoundField>
        <asp:BoundField HeaderText="Notes" DataField="XmasNotes" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
        <asp:BoundField HeaderText="Enter Bike Raffle" DataField="BikeRaffle" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
        <asp:BoundField HeaderText="Bike Awarded" DataField="BikeAwardedDate" DataFormatString="{0:d}" ItemStyle-VerticalAlign="Top" />

        </Columns>
        </asp:GridView>


 </div>  




<div class="dnnForm" id="form-AFM-record">


   <fieldset>
	<dnn:sectionhead id="sectGeneralSettingsAFM" cssclass="Head" runat="server" text="Additional Family Member" section="AFMGeneralSection" includerule="False" isexpanded="True" />

		<div id="AFMGeneralSection" runat="server">   
				<div class="dnnFormItem">
					<dnn:Label runat="server" ID="lblAFM_Name" ControlName="txtAFM_FirstName" ResourceKey="lblAFM_Name" suffix=":" />
					<asp:TextBox runat="server" ID="txtAFM_FirstName" />
					<asp:RequiredFieldValidator runat="server" id="reqAFM_FirstName" ValidationGroup="AFM" CssClass="dnnFormMessage dnnFormError" controltovalidate="txtAFM_FirstName"  ResourceKey="txtAFM_FirstName.Required" />
				</div>
				<div class="dnnFormItem">
					<dnn:Label runat="server" ID="lblAFM_MiddleName" ControlName="txtAFM_MiddleInitial" ResourceKey="lblAFM_MiddleName" suffix=":" />
					<asp:TextBox ID="txtAFM_MiddleInitial" runat="server" />
				</div>
				<div class="dnnFormItem">
					<dnn:Label runat="server" ID="lblAFM_LastName" ControlName="txtAFM_LastName" ResourceKey="lblAFM_LastName" suffix=":" />
					<asp:TextBox runat="server" ID="txtAFM_LastName" />
					<asp:RequiredFieldValidator runat="server" id="reqAFM_LastName" ValidationGroup="AFM" CssClass="dnnFormMessage dnnFormError" controltovalidate="txtAFM_LastName"  ResourceKey="txtAFM_LastName.Required" />
				</div>


   <asp:Panel ID="PanelShowAFMSuffix" runat="server">
				<div class="dnnFormItem">
					<dnn:Label runat="server" ID="lblAFM_Suffix" ControlName="ddlAFMSuffix" ResourceKey="lblAFM_Suffix" suffix=":" />
					<asp:DropDownList ID="ddlAFMSuffix" runat="server"></asp:DropDownList>
				</div>
   </asp:Panel>
				<div class="dnnFormItem">
					<dnn:Label runat="server" ID="lblAFM_DOB" ControlName="txtAFM_DOB" ResourceKey="lblAFM_DOB" suffix=":" />
					<asp:TextBox ID="txtAFM_DOB" runat="server" ClientIDMode="Static" />
                    <asp:RangeValidator ID="Range2" ControlToValidate="txtAFM_DOB" 
Type="Date" Format="MM/DD/YYYY" ValidationGroup="AFM" 
CssClass="dnnFormMessage dnnFormError" 
resourcekey="reqAFMDOBRange" Display="Dynamic"
runat="server" />
                      &nbsp;<asp:CheckBox ID="cbxAFMDOB_Verified" runat="server" resourcekey="cbxAFMDOB_Verified"  Font-Size="10px"/>
					<asp:RequiredFieldValidator runat="server" id="reqAFM_DOB"  ValidationGroup="AFM" CssClass="dnnFormMessage dnnFormError"
						controltovalidate="txtAFM_DOB" ResourceKey="radAFM_DOB.Required" />
                        <asp:CustomValidator ID="CustomValidator1" Enabled="False" runat="server" ValidationGroup="AFM" CssClass="dnnFormMessage dnnFormError" ErrorMessage="*" ClientValidationFunction="ValidateCheckBox" ResourceKey="cbxAFMDOB_Verified.Required"></asp:CustomValidator>
				</div>
            <asp:Panel ID="PanelShowAFMRelationship" runat="server">
				<div class="dnnFormItem">
					<dnn:Label runat="server" ID="lblAFMRelationship" ControlName="ddlAFMRelationship" ResourceKey="lblAFMRelationship" suffix=":" />
					<asp:DropDownList ID="ddlAFMRelationship" runat="server">
						</asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvAFMRelationship" runat="server" ValidationGroup="AFM" CssClass="dnnFormMessage dnnFormError"
						controltovalidate="ddlAFMRelationship" InitialValue="" ResourceKey="rfvAFMRelationship.Required" />
				</div>
            </asp:Panel>
				<div class="dnnFormItem">
					<dnn:label id="lblAFMGender" runat="server" resourcekey="lblAFMGender" controlname="ddlAFMGender" suffix=":"></dnn:label>
					<asp:DropDownList ID="ddlAFMGender" runat="server">
					<asp:ListItem Text="----" Value=""></asp:ListItem>
                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                <asp:ListItem Value="Non-Label" Text="Non-Labeling"></asp:ListItem>
                <asp:ListItem Value="NoAnswer" Text="Prefers Not To Answer"></asp:ListItem>
					</asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvAFMGender" runat="server" ValidationGroup="AFM" CssClass="dnnFormMessage dnnFormError"
						controltovalidate="ddlAFMGender" InitialValue="" ResourceKey="rfvAFMGender.Required" />
				</div>
     
				<div class="dnnFormItem">
					<dnn:label id="lblAFMEthnicity" runat="server" resourcekey="lblAFMEthnicity" controlname="ddlAFMEthnicity" suffix=":"></dnn:label>
					<asp:DropDownList ID="ddlAFMEthnicity" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqAFMEthnicity" runat="server" CssClass="dnnFormMessage dnnFormError" ControlToValidate="ddlAFMEthnicity" InitialValue="" Enabled="false" resourcekey="reqAFMEthnicity" ValidationGroup="AFM" />
				</div>

				
		</div>		
		
    </fieldset>

</div>
   



    <asp:HiddenField ID="hidClientAFMID" runat="server" />


        </div>
<!---- END Additional Family Members TAB ----->

<!---- BEGIN Client Visits TAB ----->
<div id="Visits" class="dnnClear">
 
 <div style="position:absolute; right: 0;"><asp:LinkButton runat="server" ID="btnAddClientVisit" OnClientClick="dataChanged=0;" 
                CssClass="btn btn-primary" ResourceKey="btnAddClientVisit" ValidationGroup="VisitForm" 
                onclick="btnAddClientVisit_Click" />
     <input id="SignBtn" name="SignBtn" type="button" value="Sign" onclick="javascript:onSign();" class="btn btn-primary" />
        <input id="ClearBtn" name="ClearBtn" type="button" value="Clear" onclick="javascript:onClear();" class="btn btn-primary" style="display:none;"  />&nbsp;

        <input id="DoneBtn" name="DoneBtn" type="button" value="Save/Update Visit"  class="btn btn-primary" onclick="javascript:onDone();" style="display:none;"  />
 </div>   

        
<br clear="all" />    






<div class="dnnForm" id="form-visit">

    <fieldset>
             		
		<div class="dnnFormItem">						
					 <dnn:Label runat="server" ID="lblMobileLocations" ControlName="ddlMobileLocations" ResourceKey="lblMobileLocations" Suffix=":" /> 
 <asp:DropDownList ID="ddlMobileLocations" runat="server" EnableViewState="true" /><asp:RequiredFieldValidator runat="server" id="reqMobileLocations"  
            controltovalidate="ddlMobileLocations" InitialValue="" CssClass="dnnFormMessage dnnFormError" ResourceKey="MobileLocationsError" ValidationGroup="VisitForm" /> 
		</div>	



		<div class="dnnFormItem">
<dnn:Label runat="server" ID="lblVisitDate" ControlName="txtVisitDate" ResourceKey="lblVisitDate" Suffix=":" /> 
		 <asp:TextBox ID="txtVisitDate" runat="server" ClientIDMode="Static" /><asp:RequiredFieldValidator runat="server" id="reqVisitDate" 
            controltovalidate="txtVisitDate" CssClass="dnnFormMessage dnnFormError" ResourceKey="VisitDateRequired" ValidationGroup="VisitForm" /> 
		</div>

		<div class="dnnFormItem">
<dnn:Label runat="server" ID="lblVisitNumBags" ControlName="ddlVisitNumBags" ResourceKey="lblVisitNumBags" Suffix=":"  /> 
		  <asp:DropDownList ID="ddlVisitNumBags" runat="server">
            <asp:ListItem Text="0" Value="0"></asp:ListItem>
            <asp:ListItem Text="1" Value="1"></asp:ListItem>
            <asp:ListItem Text="2" Value="2"></asp:ListItem>
            <asp:ListItem Text="3" Value="3"></asp:ListItem>
            <asp:ListItem Text="4" Value="4"></asp:ListItem>
            <asp:ListItem Text="5" Value="5"></asp:ListItem>
            <asp:ListItem Text="6" Value="6"></asp:ListItem>
            <asp:ListItem Text="7" Value="7"></asp:ListItem>
            <asp:ListItem Text="8" Value="8"></asp:ListItem>
            <asp:ListItem Text="9" Value="9"></asp:ListItem>
            <asp:ListItem Text="10" Value="10"></asp:ListItem>
               <asp:ListItem Text="11" Value="11"></asp:ListItem>
 <asp:ListItem Text="12" Value="12"></asp:ListItem>
 <asp:ListItem Text="13" Value="13"></asp:ListItem>
 <asp:ListItem Text="14" Value="14"></asp:ListItem>
 <asp:ListItem Text="15" Value="15"></asp:ListItem>
            </asp:DropDownList> 
		</div>

<asp:Panel ID="PanelNumTimesInsterted" runat="server" Visible="false">
		<div class="dnnFormItem">
<dnn:Label runat="server" ID="lblNumTimesInsterted" ControlName="ddlNumTimesInsterted" ResourceKey="lblNumTimesInsterted" Suffix=":"  /> 
		  <asp:DropDownList ID="ddlNumTimesInsterted" runat="server">
            
            <asp:ListItem Text="1" Value="1"  Selected="True"></asp:ListItem>
            <asp:ListItem Text="2" Value="2"></asp:ListItem>
            <asp:ListItem Text="3" Value="3"></asp:ListItem>
            <asp:ListItem Text="4" Value="4"></asp:ListItem>
            <asp:ListItem Text="5" Value="5"></asp:ListItem>
            <asp:ListItem Text="6" Value="6"></asp:ListItem>
            <asp:ListItem Text="7" Value="7"></asp:ListItem>
            <asp:ListItem Text="8" Value="8"></asp:ListItem>
            <asp:ListItem Text="9" Value="9"></asp:ListItem>
            <asp:ListItem Text="10" Value="10"></asp:ListItem>
			<asp:ListItem Text="11" Value="11"></asp:ListItem>
            <asp:ListItem Text="12" Value="12"></asp:ListItem>
            <asp:ListItem Text="13" Value="13"></asp:ListItem>
            <asp:ListItem Text="14" Value="14"></asp:ListItem>
            <asp:ListItem Text="15" Value="15"></asp:ListItem>
            <asp:ListItem Text="16" Value="16"></asp:ListItem>
            <asp:ListItem Text="17" Value="17"></asp:ListItem>
            <asp:ListItem Text="18" Value="18"></asp:ListItem>
            <asp:ListItem Text="19" Value="19"></asp:ListItem>
            <asp:ListItem Text="20" Value="20"></asp:ListItem>
            </asp:DropDownList> 
		</div>
		
</asp:Panel>
        <div id="SigDiv" class="dnnFormItem" style="display:none;" runat="server">
            <dnn:Label runat="server" ID="lblSignature" ControlName="cnv" ResourceKey="lblSignature" Suffix=":"  /> 
<canvas id="cnv" name="cnv" width="443" height="100" style="border: 2px solid #ddd;"></canvas>
            <asp:HiddenField ID="HiddenFieldSignData" runat="server" />
          <asp:HiddenField ID="HiddenFieldImgData" runat="server" />
            </div>

        <div class="dnnFormItem">
<dnn:Label runat="server" ID="lblSignatureOnFile" ControlName="ImgInitials" ResourceKey="lblSignatureOnFile" Suffix=":" Visible="false" /> 
<asp:Image ID="ImgInitials" runat="server" Visible="false" />

        </div>

		<div class="dnnFormItem">
 <dnn:Label runat="server" ID="lblVisitNotes" ControlName="txtVisitNotes" ResourceKey="lblVisitNotes" Suffix=":" /> 
		 <asp:TextBox runat="server" ID="txtVisitNotes" TextMode="MultiLine" Width="300" Rows="6" /> 
		</div>




    </fieldset>

</div>        



    <asp:HiddenField ID="hidVisitID" runat="server" />

    <asp:HiddenField ID="hidClientCellNumber" runat="server" />


<asp:GridView ID="gvVisits" runat="server" 
    DataKeyNames="VisitID" OnRowEditing="gvVisits_RowEditing" OnRowCommand="gvVisits_RowCommand" 
    OnPageIndexChanging="gvVisits_PageIndexChanging" OnRowDataBound="gvVisits_RowDataBound"          
    AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-list"  
    resourcekey="gvVisits" AllowPaging="True" PageSize="5">
<AlternatingRowStyle CssClass="alt" />    
<PagerStyle CssClass="pagination-ys" />   
<PagerSettings Mode="NumericFirstLast" /> 
    <Columns>
          <asp:TemplateField HeaderText="Send Text" ItemStyle-VerticalAlign="Top" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonSendOrderSheet" CausesValidation="False"     
             CommandArgument='<%# Eval("VisitID") + "-" + Eval("ClientID")%>' CommandName="SendOrderSheet" runat="server"><asp:image ID="imgSendOrderSheet" runat="server" imageurl="~/Icons/Sigma/Email_32x32_Standard.png" AlternateText="Send Order Sheet" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="Top" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonEdit" CausesValidation="False"     
             CommandArgument='<%# Eval("VisitID") %>' 
             CommandName="Edit" runat="server"><asp:image ID="imgEdit" runat="server" imageurl="~/Icons/Sigma/Edit_32X32_Standard.png" AlternateText="Edit Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

        <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="Top" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
         <ItemTemplate>
           <asp:LinkButton ID="LinkButtonDelete" CausesValidation="False"     
             CommandArgument='<%# Eval("VisitID") %>' 
             CommandName="DeleteVisit" runat="server"><asp:image ID="imgDeleteVisit" runat="server" imageurl="~/Icons/Sigma/Delete_32X32_Standard.png" AlternateText="Delete Record" /></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

      <asp:BoundField HeaderText="Visit Date" ItemStyle-VerticalAlign="Top" DataField="VisitDate" DataFormatString="{0:MM/dd/yyyy}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
       <asp:BoundField HeaderText="Notes" DataField="VisitNotes" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
        <asp:TemplateField HeaderText="Signature" ItemStyle-VerticalAlign="Top" ItemStyle-Width="150px">    
            <ItemTemplate><asp:Image ID="ImageSignature" CssClass="sigZoom" runat="server" Height="30px" ImageUrl='<%# "data:image/png;base64," + Convert.ToBase64String((byte[])Eval("ClientSignature"))%>'  Width="150px"></asp:Image>      
          
            </ItemTemplate>
        </asp:TemplateField>	
        <asp:BoundField HeaderText="# of Bags" DataField="VisitNumBags" ItemStyle-VerticalAlign="Top" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField HeaderText="Service Location" DataField="ServiceLocation" ItemStyle-VerticalAlign="Top" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
        <asp:BoundField HeaderText="Recorded By" DataField="LastModifiedByUserName" ItemStyle-VerticalAlign="Top" ItemStyle-Width="160px"  ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    </Columns>
</asp:GridView>	

		
</div>
<!---- END Client Visits TAB ----->

<div style="text-align:right;"><asp:linkbutton cssclass="btn btn-default" id="cmdCancel" resourcekey="cmdCancel"  
        runat="server" text="Cancel" causesvalidation="False"  
        OnClick="cmdCancel_Click"></asp:linkbutton></div>






  </div>


  <script type="text/javascript" language="javascript" >
      function requireAddressVerifyDate() {


          var rfvAddressVerifyDate = document.getElementById('<%= rfvAddressVerifyDate.ClientID %>');
          var isItChecked = $('#<%= cbxClientAddressVerify.ClientID %>').prop('checked')  // Boolean true
          ValidatorEnable(rfvAddressVerifyDate, isItChecked);

          //          if (isItChecked) {
          //              ValidatorEnable(rfvAddressVerifyDate, isItChecked);
          //              alert("1");
          //          }
          //          else {
          //              ValidatorEnable(rfvAddressVerifyDate, isItChecked);
          //              alert("2");
          //          }


      }

      function requireClientVerifyDate() {


          var rfvClientVerifyDate = document.getElementById('<%= rfvClientVerifyDate.ClientID %>');
          var isItChecked = $('#<%= cbxDOB_Verified.ClientID %>').prop('checked')  // Boolean true
          ValidatorEnable(rfvClientVerifyDate, isItChecked);
          
         

      }
</script>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UPS API Test Page</title>
    <link rel="stylesheet" href="css/style.css" />
</head>
<body>
    <form runat="server">
	    <div>
            <input type="text" id="upsTrackNo" placeholder="ups tracking #" runat="server" />
			<asp:Button ID="upsTrackButton" Text="Submit" OnClick="GetUpsTrackingDetails" runat="server" />
        </div>
        <div id="MessageArea" runat="server" class="container MessageArea"></div>
        <div id="ShowResults" runat="server" class="container ShowResults">
            <div id="shipmentInfo" class="container Info">
                <div>
                    <b>ID #</b>
                    <div id="shipID" runat="server"></div>
                </div>
                <div>
                    <b>Shipment Pieces</b>
                    <div id="shipPieces" runat="server"></div>
                </div>
                <div>
                    <b>Shipment Weight</b>
                    <div id="shipWeight" runat="server"></div>
                </div>
                <div>
                    <b>Tracking #</b>
                    <div id="trackingNumber" runat="server"></div>
                </div>
                <div>
                    <b>Piece Weight</b>
                    <div id="pieceWeight" runat="server"></div>
                </div>
                <div id="pieceDimensionsArea" runat="server">
                    <b>Piece Dimensions</b>
                    <div id="pieceDimensions" runat="server"></div>
                </div>
                <div id="PONumberArea" runat="server">
                    <b>PO Number</b>
                    <div id="PONumber" runat="server"></div>
                </div>
                <div id="ReferenceNumberArea" runat="server">
                    <b>Reference Number</b>
                    <div id="ReferenceNumber" runat="server"></div>
                </div>
            </div>
            <div id="shipperInfo" class="container Info">
                <div>
                    <b>UPS Customer #</b>
                    <div id="upsCustNum" runat="server"></div>
                </div>
                <div>
                    <b>Address1</b>
                    <div id="shipperAddress1" runat="server"></div>
                </div>
                <div>
                    <b>Address2</b>
                    <div id="shipperAddress2" runat="server"></div>
                </div>
                <div>
                    <b>Address3</b>
                    <div id="shipperAddress3" runat="server"></div>
                </div>
                <div>
                    <b>City</b>
                    <div id="shipperCity" runat="server"></div>
                </div>
                <div>
                    <b>State</b>
                    <div id="shipperState" runat="server"></div>
                </div>
                <div>
                    <b>Zip</b>
                    <div id="shipperZip" runat="server"></div>
                </div>
                <div>
                    <b>Country</b>
                    <div id="shipperCountry" runat="server"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

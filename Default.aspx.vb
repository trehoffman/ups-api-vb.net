
Partial Class _Default
    Inherits System.Web.UI.Page

    Sub GetUpsTrackingDetails()
        Dim trackNo As String = upsTrackNo.Value
        Dim ups As New UPS(trackNo)

        'Response.Write(UPS.getXmlString(trackNo))
        'Response.Write(UPS.getAllTrackingNumbersInShipment(trackNo))

        If ups.getIdentificationNumber() <> "" Then
            shipID.InnerText = ups.getIdentificationNumber()
            shipPieces.InnerText = ups.getPieceCount()
            shipWeight.InnerText = ups.getShipmentWeight()
            trackingNumber.InnerHtml = Replace(ups.getAllTrackingNumbersInShipment(), " ", "<br>")
            pieceWeight.InnerText = ups.getPieceWeight()
            upsCustNum.InnerText = ups.getUpsShipperNumber()
            shipperAddress1.InnerText = ups.getShipperAddressElement("AddressLine1")
            shipperAddress2.InnerText = ups.getShipperAddressElement("AddressLine2")
            shipperAddress3.InnerText = ups.getShipperAddressElement("AddressLine3")
            shipperCity.InnerText = ups.getShipperAddressElement("City")
            shipperState.InnerText = ups.getShipperAddressElement("StateProvinceCode")
            shipperZip.InnerText = ups.getShipperAddressElement("PostalCode")
            shipperCountry.InnerText = ups.getShipperAddressElement("CountryCode")
            MessageArea.Style("display") = "none"
            pieceDimensionsArea.Style("display") = "none"
            PONumberArea.Style("display") = "none"
            ReferenceNumberArea.Style("display") = "none"
            ShowResults.Style("display") = "block"
        Else
            MessageArea.InnerText = "Tracking Number " & trackNo & " not found."
            MessageArea.Style("display") = "block"
            ShowResults.Style("display") = "none"
        End If
    End Sub
End Class

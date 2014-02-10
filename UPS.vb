Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Net

Public Class UPS
    Private accessKey As String = ""
    Private userName As String = ""
    Private passWord As String = ""
    Private path As String = "https://www.ups.com/ups.app/xml/Track"

    'public functions

    'getIdentificationNumber might not be necessary
    Public Shared Function getIdentificationNumber(trackNo As String) As String
        Dim ups As New UPS
        Dim xml As XmlDocument = ups.getUPSXMLbyTrackingNumber(trackNo)
        Dim idNo As String = ups.getNodeValue(xml, "TrackResponse/Shipment/ShipmentIdentificationNumber")

        Return idNo.Trim
    End Function

    Public Shared Function getPieceCount(trackNo As String) As Integer
        Dim ups As New UPS
        Dim idNo As String = getIdentificationNumber(trackNo)
        Dim xml As XmlDocument = ups.getUPSXMLbyShipmentIdentificationNumber(idNo)
        Dim numString As String = ups.getNodeCount(xml, "TrackResponse/Shipment/Package")

        If numString = "" Then
            numString = "0"
        End If

        Return CInt(numString.Trim)
    End Function

    Public Shared Function getShipmentWeight(trackNo As String) As Double
        Dim ups As New UPS
        Dim idNo As String = getIdentificationNumber(trackNo)
        Dim xml As XmlDocument = ups.getUPSXMLbyShipmentIdentificationNumber(idNo)
        Dim numString As String = ups.getNodeValue(xml, "TrackResponse/Shipment/ShipmentWeight/Weight")

        If numString = "" Then
            numString = "0"
        End If

        Return CDbl(numString.Trim)
    End Function

    Public Shared Function getPieceWeight(trackNo As String) As Double
        Dim ups As New UPS
        Dim xml As XmlDocument = ups.getUPSXMLbyTrackingNumber(trackNo)
        Dim numString As String = ups.getNodeValue(xml, "TrackResponse/Shipment/Package/PackageWeight/Weight")

        If numString = "" Then
            numString = "0"
        End If

        Return CDbl(numString.Trim)
    End Function

    Public Shared Function getUPSShipperNumber(trackNo As String) As String
        Dim ups As New UPS
        Dim xml As XmlDocument = ups.getUPSXMLbyTrackingNumber(trackNo)
        Dim cnum As String = ups.getNodeValue(xml, "TrackResponse/Shipment/Shipper/ShipperNumber")

        Return cnum.Trim
    End Function

    Public Shared Function getShipperAddressElement(trackNo As String, element As String) As String
        Dim ups As New UPS
        Dim xml As XmlDocument = ups.getUPSXMLbyTrackingNumber(trackNo)
        Dim strOut As String = ups.getNodeValue(xml, "TrackResponse/Shipment/Shipper/Address/" & element)

        Return strOut.Trim
    End Function

    'private "helper" functions

    Private Function getUPSXMLbyShipmentIdentificationNumber(idNo As String) As XmlDocument
        Dim xml_req As String = "<?xml version='1.0'?>" & _
              "<AccessRequest xml:lang='en-US'>" & _
               "<AccessLicenseNumber>" & accessKey & "</AccessLicenseNumber>" & _
               "<UserId>" & userName & "</UserId>" & _
               "<Password>" & passWord & "</Password>" & _
              "</AccessRequest>" & _
              "<?xml version='1.0'?>" & _
              "<TrackRequest xml:lang='en-US'>" & _
               "<Request>" & _
                "<TransactionReference>" & _
                 "<CustomerContext>QAST Track</CustomerContext>" & _
                 "<XpciVersion>1.0</XpciVersion>" & _
                "</TransactionReference>" & _
                "<RequestAction>Track</RequestAction>" & _
                "<RequestOption>activity</RequestOption>" & _
               "</Request>" & _
               "<ShipmentIdentificationNumber>" & idNo & "</ShipmentIdentificationNumber>" & _
              "</TrackRequest>"

        Dim client As New WebClient()
        client.Headers.Add("Content-Type", "application/xml")
        Dim sentByte As Byte() = System.Text.Encoding.ASCII.GetBytes(xml_req)
        Dim responseByte As Byte() = client.UploadData(path, "POST", sentByte)

        Dim responseString As String = System.Text.Encoding.ASCII.GetString(responseByte)
        Dim responseXML As New XmlDocument()
        responseXML.LoadXml(responseString)

        Return responseXML
    End Function

    Private Function getUPSXMLbyTrackingNumber(trackNo As String) As XmlDocument
        Dim xml_req As String = "<?xml version='1.0'?>" & _
              "<AccessRequest xml:lang='en-US'>" & _
               "<AccessLicenseNumber>" & accessKey & "</AccessLicenseNumber>" & _
               "<UserId>" & userName & "</UserId>" & _
               "<Password>" & passWord & "</Password>" & _
              "</AccessRequest>" & _
              "<?xml version='1.0'?>" & _
              "<TrackRequest xml:lang='en-US'>" & _
               "<Request>" & _
                "<TransactionReference>" & _
                 "<CustomerContext>QAST Track</CustomerContext>" & _
                 "<XpciVersion>1.0</XpciVersion>" & _
                "</TransactionReference>" & _
                "<RequestAction>Track</RequestAction>" & _
                "<RequestOption>activity</RequestOption>" & _
               "</Request>" & _
               "<TrackingNumber>" & trackNo & "</TrackingNumber>" & _
              "</TrackRequest>"

        Dim client As New WebClient()
        client.Headers.Add("Content-Type", "application/xml")
        Dim sentByte As Byte() = System.Text.Encoding.ASCII.GetBytes(xml_req)
        Dim responseByte As Byte() = client.UploadData(path, "POST", sentByte)

        Dim responseString As String = System.Text.Encoding.ASCII.GetString(responseByte)
        Dim responseXML As New XmlDocument()
        responseXML.LoadXml(responseString)

        Return responseXML
    End Function

    Private Function getNodeValue(xml As XmlDocument, nodePath As String) As String
        Dim node As XmlNode = xml.SelectSingleNode(nodePath)

        If Not node Is Nothing Then
            Return node.InnerText.Trim
        End If

        Return ""
    End Function

    Private Function getNodeCount(xml As XmlDocument, nodePath As String) As Integer
        Dim nodeList = xml.SelectNodes(nodePath)
        Return nodeList.Count
    End Function
End Class

Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Net

Public Class UPS
    Private accessKey As String = "XXXXXXXXXXXXXXXX"
    Private userName As String = "username"
    Private passWord As String = "password"
    Private path As String = "https://www.ups.com/ups.app/xml/Track"
    Public xml As XmlDocument = New XmlDocument()

    'constructor
    Sub New(trackNo As String)
        xml = getUPSXMLbyTrackingNumber(trackNo)
    End Sub

    'public functions
    'getIdentificationNumber might not be necessary
    Public Function getIdentificationNumber() As String
        Dim idNo As String = getNodeValue(xml, "TrackResponse/Shipment/ShipmentIdentificationNumber")

        Return idNo.Trim
    End Function

    Public Function getPieceCount() As Integer
        'Dim idNo As String = getIdentificationNumber()
        Dim numString As String = getNodeCount(xml, "TrackResponse/Shipment/Package")

        If numString = "" Then
            numString = "0"
        End If

        Return CInt(numString.Trim)
    End Function

    Public Function getShipmentWeight() As Double
        'Dim idNo As String = getIdentificationNumber()
        Dim numString As String = getNodeValue(xml, "TrackResponse/Shipment/ShipmentWeight/Weight")

        If numString = "" Then
            numString = "0"
        End If

        Return CDbl(numString.Trim)
    End Function

    Public Function getPieceWeight() As Double
        Dim numString As String = getNodeValue(xml, "TrackResponse/Shipment/Package/PackageWeight/Weight")

        If numString = "" Then
            numString = "0"
        End If

        Return CDbl(numString.Trim)
    End Function

    Public Function getUpsShipperNumber() As String
        Dim cnum As String = getNodeValue(xml, "TrackResponse/Shipment/Shipper/ShipperNumber")

        Return cnum.Trim
    End Function

    Public Function getShipperAddressElement(element As String) As String
        Dim strOut As String = getNodeValue(xml, "TrackResponse/Shipment/Shipper/Address/" & element)

        Return strOut.Trim
    End Function

    Public Function getAllTrackingNumbersInShipment() As String
        Dim txtOut As String = ""
        'Dim idNo As String = getIdentificationNumber(trackNo)
        'Dim idNo As String = trackNo
        'Dim xml As XmlDocument = ups.getUPSXMLbyShipmentIdentificationNumber(idNo)
        Dim nodeList As XmlNodeList = getNodeList(xml, "TrackResponse/Shipment/Package")

        For Each n In nodeList
            'Dim namespaces As XmlNamespaceManager = New XmlNamespaceManager(xml.NameTable)
            Dim node As XmlNode = n.SelectSingleNode("TrackingNumber")
            txtOut &= node.InnerText & " "
        Next

        Return txtOut.Trim
    End Function

    Public Function getXmlString(trackNo As String) As String
        Dim xml_req As String = "<?xml version='1.0'?>" &
              "<AccessRequest xml:lang='en-US'>" &
               "<AccessLicenseNumber>" & accessKey & "</AccessLicenseNumber>" &
               "<UserId>" & userName & "</UserId>" &
               "<Password>" & passWord & "</Password>" &
              "</AccessRequest>" &
              "<?xml version='1.0'?>" &
              "<TrackRequest xml:lang='en-US'>" &
               "<Request>" &
                "<TransactionReference>" &
                 "<CustomerContext>QAST Track</CustomerContext>" &
                 "<XpciVersion>1.0</XpciVersion>" &
                "</TransactionReference>" &
                "<RequestAction>Track</RequestAction>" &
                "<RequestOption>activity</RequestOption>" &
               "</Request>" &
               "<ShipmentIdentificationNumber>" & trackNo & "</ShipmentIdentificationNumber>" &
              "</TrackRequest>"

        Dim client As New WebClient()
        client.Headers.Add("Content-Type", "application/xml")
        Dim sentByte As Byte() = System.Text.Encoding.ASCII.GetBytes(xml_req)
        Dim responseByte As Byte() = client.UploadData("https://www.ups.com/ups.app/xml/Track", "POST", sentByte)

        Dim responseString As String = System.Text.Encoding.ASCII.GetString(responseByte)
        'Dim responseXML As New XmlDocument()
        'responseXML.LoadXml(responseString)

        Return responseString
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

    Private Function getNodeList(xml As XmlDocument, nodePath As String) As XmlNodeList
        'Dim namespaces As XmlNamespaceManager = New XmlNamespaceManager(xml.NameTable)
        Dim nodeList As XmlNodeList = xml.SelectNodes(nodePath)

        Return nodeList
    End Function
End Class

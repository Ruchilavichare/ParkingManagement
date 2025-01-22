<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParkingSystem.aspx.cs" Inherits="ParkingManagentSystem.ParkingSystem" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Parking System</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f9;
        }

        .container {
            width: 80%;
            margin: auto;
            overflow: hidden;
        }

        h1 {
            text-align: center;
            color: #333;
        }

        .form-container {
            background: #fff;
            padding: 20px;
            margin: 20px 0;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
        }

        .form-container label {
            font-weight: bold;
            display: block;
            margin-bottom: 5px;
        }

        .form-container input, .form-container select, .form-container button {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .form-container button {
            background-color: #007bff;
            color: #fff;
            border: none;
            cursor: pointer;
        }

        .form-container button:hover {
            background-color: #0056b3;
        }

        .result {
            margin-top: 20px;
            background: #e9ffe9;
            padding: 15px;
            border: 1px solid #7dce94;
            color: #2d572c;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container">
            <h1>Parking System</h1>
            <div class="form-container">
                <h2>Park a Vehicle</h2>
                <asp:Label ID="lblMessage" runat="server" CssClass="result" Visible="false"></asp:Label>

                <label for="ddlGate">Select Gate ID:</label>
                <asp:DropDownList ID="ddlGate" runat="server" OnSelectedIndexChanged ="loadSpaceInfo" AutoPostBack ="true"></asp:DropDownList>

                <label for="ddlSpace">Select Space ID:</label>
                <asp:DropDownList ID="ddlSpace" runat="server"></asp:DropDownList>

                <label for="txtVehicleNumber">Vehicle Number:</label>
                <asp:TextBox ID="txtVehicleNumber" runat="server" placeholder="Enter Vehicle Number"></asp:TextBox>

                <asp:Button ID="btnParkVehicle" runat="server" Text="Park Vehicle" OnClick="btnParkVehicle_Click" />

                <hr />

                <h2>Unpark a Vehicle</h2>
                <label for="txtParkingId">Parking ID:</label>
                <asp:TextBox ID="txtParkingId" runat="server" placeholder="Enter Parking ID"></asp:TextBox>
                <asp:Button ID="btnUnparkVehicle" runat="server" Text="Unpark Vehicle" OnClick="btnUnparkVehicle_Click" />
            </div>
        </div>
    </form>
</body>
</html>
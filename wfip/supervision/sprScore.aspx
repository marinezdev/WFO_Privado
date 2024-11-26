<%@ Page Title="" Language="C#" MasterPageFile="~/supervision/supervision.Master" AutoEventWireup="true" CodeBehind="sprScore.aspx.cs" Inherits="wfip.supervision.sprScore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 15.75pt;
            width: 125pt;
            color: #203764;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom: 1.0pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style2 {
            width: 177pt;
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom: 1.0pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style3 {
            width: 99pt;
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom: 1.0pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style4 {
            width: 106pt;
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom: 1.0pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style5 {
            width: 98pt;
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom: 1.0pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style6 {
            width: 96pt;
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom: 1.0pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style7 {
            width: 90pt;
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom: 1.0pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style8 {
            width: 154pt;
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom: 1.0pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style9 {
            height: 15.0pt;
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: 1.0pt solid #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: 1.0pt solid #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style10 {
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: 1.0pt solid #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style11 {
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 2.0pt double #70AD47;
            border-top: 1.0pt solid #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style12 {
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: 1.0pt solid #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style13 {
            color: white;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 1.5pt solid #70AD47;
            border-top: 1.0pt solid #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #0070C0;
        }

        .auto-style14 {
            height: 15.0pt;
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: 1.0pt solid #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style15 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style16 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 2.0pt double #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style17 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style18 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: white;
        }

        .auto-style19 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 2.0pt double #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: white;
        }

        .auto-style20 {
            color: #203764;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }

        .auto-style21 {
            color: #203764;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }

        .auto-style22 {
            color: #203764;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 1.5pt solid #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: .5pt dotted #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }

        .auto-style23 {
            height: 15.0pt;
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: 1.0pt solid #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style24 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style25 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 2.0pt double #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style26 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style27 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: white;
        }

        .auto-style28 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 2.0pt double #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: white;
        }

        .auto-style29 {
            color: #203764;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }

        .auto-style30 {
            color: #203764;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }

        .auto-style31 {
            color: #203764;
            font-size: 8pt;
            
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 1.5pt solid #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }

        .auto-style32 {
            height: 15.75pt;
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: 1.0pt solid #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style33 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style34 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 2.0pt double #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style35 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }

        .auto-style36 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: white;
        }

        .auto-style37 {
            color: #203764;
            font-size: 8pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 2.0pt double #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: white;
        }

        .auto-style38 {
            color: #203764;
            font-size: 8pt;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }

        .auto-style39 {
            color: #203764;
            font-size: 8pt;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: .5pt dotted #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }

        .auto-style40 {
            color: #203764;
            font-size: 8pt;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: .5pt dotted #70AD47;
            border-right: 1.5pt solid #70AD47;
            border-top: .5pt dotted #70AD47;
            border-bottom: 1.5pt solid #70AD47;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #BFBFBF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_areaTrabajo" runat="server">
    <div style="width: 100%; background-color: #0094ff; font-size: 14px; text-align: center; color: white;">
        <asp:Literal ID="ltTituloGrafica" runat="server"></asp:Literal>
    </div>
    <asp:Panel ID="pnlScore" runat="server" Width="100%" ScrollBars="Horizontal">
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 1041pt; font-size: 8px;"
            width="1385">
            <colgroup>
                <col style="mso-width-source: userset; mso-width-alt: 6070; width: 125pt" width="166" />
                <col style="mso-width-source: userset; mso-width-alt: 2779; width: 57pt" width="76" />
                <col style="mso-width-source: userset; mso-width-alt: 5851; width: 120pt" width="160" />
                <col style="mso-width-source: userset; mso-width-alt: 2084; width: 43pt" width="57" />
                <col style="mso-width-source: userset; mso-width-alt: 2706; width: 56pt" width="74" />
                <col style="mso-width-source: userset; mso-width-alt: 2377; width: 49pt" width="65" />
                <col style="mso-width-source: userset; mso-width-alt: 2779; width: 57pt" width="76" />
                <col span="3" style="mso-width-source: userset; mso-width-alt: 2377; width: 49pt"
                    width="65" />
                <col style="mso-width-source: userset; mso-width-alt: 2304; width: 47pt" width="63" />
                <col style="mso-width-source: userset; mso-width-alt: 2084; width: 43pt" width="57" />
                <col style="mso-width-source: userset; mso-width-alt: 2304; width: 47pt" width="63" />
                <col style="mso-width-source: userset; mso-width-alt: 2377; width: 49pt" width="65" />
                <col style="mso-width-source: userset; mso-width-alt: 2304; width: 47pt" width="63" />
                <col style="mso-width-source: userset; mso-width-alt: 2377; width: 49pt" width="65" />
                <col style="mso-width-source: userset; mso-width-alt: 2304; width: 47pt" width="63" />
                <col style="mso-width-source: userset; mso-width-alt: 2816; width: 58pt" width="77" />
            </colgroup>
            <tr height="21">
                <td class="auto-style1" height="21" width="166">&nbsp;</td>
                <td class="auto-style2" colspan="2" width="236">ADMISIÓN<span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style3" colspan="2" width="131">REVISIÓN DOCUMENTAL</td>
                <td class="auto-style4" colspan="2" width="141">PLAD</td>
                <td class="auto-style5" colspan="2" width="130">SELECCIÓN</td>
                <td class="auto-style6" colspan="2" width="128">TÉCNICO</td>
                <td class="auto-style7" colspan="2" width="120">EJECUCIÓN</td>
                <td class="auto-style6" colspan="2" width="128">MÉDICO<span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style8" colspan="3" width="205">Acumulado</td>
            </tr>
            <tr height="20">
                <td class="auto-style9" height="20">PRODUCTOS</td>
                <td class="auto-style10">Espera</td>
                <td class="auto-style11">Atención</td>
                <td class="auto-style12">Espera</td>
                <td class="auto-style11">Atención</td>
                <td class="auto-style12">Espera</td>
                <td class="auto-style11">Atención</td>
                <td class="auto-style12">Espera</td>
                <td class="auto-style11">Atención</td>
                <td class="auto-style12">Espera</td>
                <td class="auto-style11">Atención</td>
                <td class="auto-style12">Espera</td>
                <td class="auto-style11">Atención</td>
                <td class="auto-style12">Espera</td>
                <td class="auto-style11">Atención</td>
                <td class="auto-style12">Espera</td>
                <td class="auto-style10">Atención</td>
                <td class="auto-style13">Acumulado</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Tempolife</td>
                <td class="auto-style15">2:39:19</td>
                <td class="auto-style16">0:09:02</td>
                <td class="auto-style17">3:34:20</td>
                <td class="auto-style16">0:26:53</td>
                <td class="auto-style17">1:16:26</td>
                <td class="auto-style16">0:12:56</td>
                <td class="auto-style17">17:21:28</td>
                <td class="auto-style16">0:44:41</td>
                <td class="auto-style17">8:57:27</td>
                <td class="auto-style16">0:00:05</td>
                <td class="auto-style17">7:19:04</td>
                <td class="auto-style16">2:47:29</td>
                <td class="auto-style18">19:16:59</td>
                <td class="auto-style19">4:12:34</td>
                <td class="auto-style20">7:15:51</td>
                <td class="auto-style21">0:50:23</td>
                <td class="auto-style22">8:06:14</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Totalife</td>
                <td class="auto-style15">1:39:53</td>
                <td class="auto-style16">0:05:48</td>
                <td class="auto-style17">5:53:40</td>
                <td class="auto-style16">0:17:36</td>
                <td class="auto-style17">1:52:37</td>
                <td class="auto-style16">0:03:05</td>
                <td class="auto-style17">18:40:25</td>
                <td class="auto-style16">1:55:15</td>
                <td class="auto-style17">7:26:04</td>
                <td class="auto-style16">0:00:02</td>
                <td class="auto-style17">10:56:57</td>
                <td class="auto-style16">2:15:06</td>
                <td class="auto-style18">16:59:49</td>
                <td class="auto-style19">0:32:53</td>
                <td class="auto-style20">8:03:20</td>
                <td class="auto-style21">0:45:42</td>
                <td class="auto-style22">8:49:03</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Perfectlife</td>
                <td class="auto-style15">3:06:41</td>
                <td class="auto-style16">0:16:24</td>
                <td class="auto-style17">4:25:24</td>
                <td class="auto-style16">0:29:03</td>
                <td class="auto-style17">1:54:01</td>
                <td class="auto-style16">0:14:46</td>
                <td class="auto-style17">12:24:22</td>
                <td class="auto-style16">0:21:52</td>
                <td class="auto-style17">3:15:42</td>
                <td class="auto-style16">0:00:01</td>
                <td class="auto-style17">11:06:44</td>
                <td class="auto-style16">2:52:24</td>
                <td class="auto-style18">4:01:26</td>
                <td class="auto-style19">1:07:40</td>
                <td class="auto-style20">5:57:28</td>
                <td class="auto-style21">0:43:24</td>
                <td class="auto-style22">6:40:52</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Horizonte Metlife Retiro</td>
                <td class="auto-style15">0:42:11</td>
                <td class="auto-style16">0:09:45</td>
                <td class="auto-style17">1:19:04</td>
                <td class="auto-style16">0:19:23</td>
                <td class="auto-style17">1:15:14</td>
                <td class="auto-style16">0:03:29</td>
                <td class="auto-style17">18:54:10</td>
                <td class="auto-style16">0:10:17</td>
                <td class="auto-style17" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style16" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style17">7:24:47</td>
                <td class="auto-style16">5:16:23</td>
                <td class="auto-style18" style="text-underline-style: none; text-line-through: none; mso-pattern: black none">0:00:00</td>
                <td class="auto-style19" style="text-underline-style: none; text-line-through: none; mso-pattern: black none">0:00:00</td>
                <td class="auto-style20">4:37:54</td>
                <td class="auto-style21">0:56:14</td>
                <td class="auto-style22">5:34:08</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Horizonte Metlife CPEA</td>
                <td class="auto-style15" style="text-underline-style: none; text-line-through: none;">&nbsp;</td>
                <td class="auto-style16">0:08:43</td>
                <td class="auto-style17">4:50:19</td>
                <td class="auto-style16">0:21:51</td>
                <td class="auto-style17">1:20:10</td>
                <td class="auto-style16">0:03:17</td>
                <td class="auto-style17">22:04:25</td>
                <td class="auto-style16">0:07:39</td>
                <td class="auto-style17">20:54:51</td>
                <td class="auto-style16" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style17">11:20:31</td>
                <td class="auto-style16">0:56:45</td>
                <td class="auto-style18">51:12:49</td>
                <td class="auto-style19">1:30:27</td>
                <td class="auto-style20">11:25:31</td>
                <td class="auto-style21">0:17:48</td>
                <td class="auto-style22">11:43:19</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">FlexiLife Protección</td>
                <td class="auto-style15">0:46:01</td>
                <td class="auto-style16">0:05:30</td>
                <td class="auto-style17">4:43:28</td>
                <td class="auto-style16">2:54:32</td>
                <td class="auto-style17">1:04:17</td>
                <td class="auto-style16">0:05:34</td>
                <td class="auto-style17">8:04:06</td>
                <td class="auto-style16">0:00:48</td>
                <td class="auto-style17" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style16" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style17">5:16:46</td>
                <td class="auto-style16">3:02:35</td>
                <td class="auto-style18">&nbsp;</td>
                <td class="auto-style19">&nbsp;</td>
                <td class="auto-style20">3:19:06</td>
                <td class="auto-style21">1:01:30</td>
                <td class="auto-style22">4:20:36</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Flexilife Sueños</td>
                <td class="auto-style15">0:23:24</td>
                <td class="auto-style16">0:01:52</td>
                <td class="auto-style17">5:20:42</td>
                <td class="auto-style16">0:04:39</td>
                <td class="auto-style17">1:25:20</td>
                <td class="auto-style16">0:00:59</td>
                <td class="auto-style17">6:04:21</td>
                <td class="auto-style16">0:04:25</td>
                <td class="auto-style17" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style16" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style17">15:49:48</td>
                <td class="auto-style16">1:00:38</td>
                <td class="auto-style18">1:57:21</td>
                <td class="auto-style19">45:17:12</td>
                <td class="auto-style20">4:43:40</td>
                <td class="auto-style21">2:00:18</td>
                <td class="auto-style22">6:43:58</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Educalife</td>
                <td class="auto-style15">3:12:02</td>
                <td class="auto-style16">0:11:13</td>
                <td class="auto-style17">4:39:06</td>
                <td class="auto-style16">3:07:58</td>
                <td class="auto-style17">1:25:10</td>
                <td class="auto-style16">0:04:34</td>
                <td class="auto-style17">15:12:58</td>
                <td class="auto-style16">2:53:41</td>
                <td class="auto-style17">1:34:30</td>
                <td class="auto-style16">0:04:09</td>
                <td class="auto-style17">12:31:04</td>
                <td class="auto-style16">7:13:29</td>
                <td class="auto-style18">1:16:58</td>
                <td class="auto-style19">0:09:26</td>
                <td class="auto-style20">6:15:09</td>
                <td class="auto-style21">2:11:29</td>
                <td class="auto-style22">8:26:38</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Flexilife</td>
                <td class="auto-style15">1:51:13</td>
                <td class="auto-style16">0:08:49</td>
                <td class="auto-style17">4:27:30</td>
                <td class="auto-style16">2:59:05</td>
                <td class="auto-style17">1:39:25</td>
                <td class="auto-style16">0:03:07</td>
                <td class="auto-style17">9:46:26</td>
                <td class="auto-style16">0:03:42</td>
                <td class="auto-style17" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style16" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style17">8:29:09</td>
                <td class="auto-style16">1:32:03</td>
                <td class="auto-style18">15:25:42</td>
                <td class="auto-style19">9:01:30</td>
                <td class="auto-style20">4:26:31</td>
                <td class="auto-style21">0:50:56</td>
                <td class="auto-style22">5:17:27</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Cambio de forma de pago</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style18">&nbsp;</td>
                <td class="auto-style19">&nbsp;</td>
                <td class="auto-style20"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style21"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style22"><span style="mso-spacerun: yes">&nbsp;</span></td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Cáncer</td>
                <td class="auto-style15">0:25:32</td>
                <td class="auto-style16">0:13:18</td>
                <td class="auto-style17">0:29:13</td>
                <td class="auto-style16">0:24:02</td>
                <td class="auto-style17">0:15:01</td>
                <td class="auto-style16">0:05:14</td>
                <td class="auto-style17" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style16" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style17" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style16" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style17">86:48:56</td>
                <td class="auto-style16">1:37:27</td>
                <td class="auto-style18">&nbsp;</td>
                <td class="auto-style19">&nbsp;</td>
                <td class="auto-style20">14:39:47</td>
                <td class="auto-style21">0:23:20</td>
                <td class="auto-style22">15:03:07</td>
            </tr>
            <tr height="20">
                <td class="auto-style14" height="20">Metalife</td>
                <td class="auto-style15">3:41:23</td>
                <td class="auto-style16">0:07:38</td>
                <td class="auto-style17">5:23:00</td>
                <td class="auto-style16">0:25:49</td>
                <td class="auto-style17">2:32:54</td>
                <td class="auto-style16">0:05:40</td>
                <td class="auto-style17">25:28:53</td>
                <td class="auto-style16">0:17:07</td>
                <td class="auto-style17">2:34:58</td>
                <td class="auto-style16">0:00:14</td>
                <td class="auto-style17">6:31:12</td>
                <td class="auto-style16">1:48:57</td>
                <td class="auto-style18">14:52:55</td>
                <td class="auto-style19">2:24:25</td>
                <td class="auto-style20">7:52:52</td>
                <td class="auto-style21">0:30:30</td>
                <td class="auto-style22">8:23:22</td>
            </tr>
            <tr height="20">
                <td class="auto-style23" height="20">Reconsideración de dictamen</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style17">&nbsp;</td>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style18">&nbsp;</td>
                <td class="auto-style19">&nbsp;</td>
                <td class="auto-style20"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style21"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style22"><span style="mso-spacerun: yes">&nbsp;</span></td>
            </tr>
            <tr height="20">
                <td class="auto-style23" height="20">Inclusión o exclusión de beneficios adicionales</td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style29"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style30"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style31"><span style="mso-spacerun: yes">&nbsp;</span></td>
            </tr>
            <tr height="20">
                <td class="auto-style23" height="20">Modificación de nombre y apellidos</td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style29"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style30"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style31"><span style="mso-spacerun: yes">&nbsp;</span></td>
            </tr>
            <tr height="20">
                <td class="auto-style23" height="20">Rescate total</td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style26">&nbsp;</td>
                <td class="auto-style25">&nbsp;</td>
                <td class="auto-style27">&nbsp;</td>
                <td class="auto-style28">&nbsp;</td>
                <td class="auto-style29"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style30"><span style="mso-spacerun: yes">&nbsp;</span></td>
                <td class="auto-style31"><span style="mso-spacerun: yes">&nbsp;</span></td>
            </tr>
            <tr height="21">
                <td class="auto-style32" height="21">Otros especificar</td>
                <td class="auto-style33">8:14:32</td>
                <td class="auto-style34">0:00:58</td>
                <td class="auto-style35">7:38:33</td>
                <td class="auto-style34">2:58:30</td>
                <td class="auto-style35">1:00:00</td>
                <td class="auto-style34">0:01:10</td>
                <td class="auto-style35">44:02:04</td>
                <td class="auto-style34">0:00:55</td>
                <td class="auto-style35" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style34" style="text-underline-style: none; text-line-through: none;">0:00:00</td>
                <td class="auto-style35">6:42:45</td>
                <td class="auto-style34">0:21:18</td>
                <td class="auto-style36">&nbsp;</td>
                <td class="auto-style37">&nbsp;</td>
                <td class="auto-style38">11:16:19</td>
                <td class="auto-style39">0:33:48</td>
                <td class="auto-style40">11:50:07</td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

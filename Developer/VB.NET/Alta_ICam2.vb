Module Alta_ICam2

    Sub Main()

        Dim FindDlg As APOGEELib.CamDiscover
        Dim AltaCamera As APOGEELib.Camera2
        Dim ImageData As Array
        Dim FileNum As Integer

        FindDlg = New APOGEELib.CamDiscover()
        AltaCamera = New APOGEELib.Camera2()
        FileNum = FreeFile()

        FindDlg.DlgCheckEthernet = True
        FindDlg.DlgCheckUsb = True

        FindDlg.ShowDialog(True)

        If FindDlg.ValidSelection Then

            AltaCamera.Init(FindDlg.SelectedInterface, FindDlg.SelectedCamIdOne, FindDlg.SelectedCamIdTwo, 0)

            AltaCamera.Expose(0.001, False)

            Do
            Loop Until AltaCamera.ImagingStatus = APOGEELib.Apn_Status.Apn_Status_ImageReady

            ImageData = AltaCamera.Image

            FileOpen(FileNum, "Image.raw", OpenMode.Binary, OpenAccess.Write)
            FilePut(FileNum, ImageData)
            FileClose(FileNum)

        End If

    End Sub

End Module

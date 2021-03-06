﻿' jobAdd.vb  July 6, 2010
' oglesbyzm@gmail.com
' This form is used to create a new new jobs for aircraft on station when the A4 page is down. If used at any other time the job will be closed
' out the next time a refresh takes place.

Public Class jobAdd

    Event NewRecordAddedEvent()
    Private Sub Archive_tblAcftOnStationBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Validate()
        Me.TableAdapterManager.UpdateAll(Me.MxDatabaseDataSet)

    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'MxDatabaseDataSet.tblPOL' table. You can move, or remove it, as needed.
        Me.TblPOLTableAdapter.Fill(Me.MxDatabaseDataSet.tblPOL)
        'TODO: This line of code loads data into the 'MxDatabaseDataSet.tblJobs' table. You can move, or remove it, as needed.
        Me.TblJobsTableAdapter.Fill(Me.MxDatabaseDataSet.tblJobs)
        'TODO: This line of code loads data into the 'MxDatabaseDataSet.tblAcftOnStation' table. You can move, or remove it, as needed.
        Me.TblAcftOnStationTableAdapter.Fill(Me.MxDatabaseDataSet.tblAcftOnStation)
        'TODO: This line of code loads data into the 'MxDatabaseDataSet.Archive_tblSOE' table. You can move, or remove it, as needed.

    End Sub

    Private Sub TblAcftOnStationBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Validate()
        Me.TblAcftOnStationBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.MxDatabaseDataSet)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim pacerValue As Boolean

        If CheckBox1.Checked Then
            pacerValue = True
        Else
            pacerValue = False
        End If

        Dim AHash As New Hashtable
        AHash.Add("PrimKey", AcftTailNumberComboBox.SelectedValue)
        AHash.Add("AcftTailNum", AcftTailNumberComboBox.Text)
        AHash.Add("JCN", TextBox1.Text)
        AHash.Add("WCE", TextBox2.Text)
        AHash.Add("Symbol", TextBox3.Text)
        AHash.Add("Narr", TextBox4.Text)
        AHash.Add("ShopZone", TextBox5.Text)
        AHash.Add("WUC", TextBox6.Text)
        AHash.Add("Pacer", pacerValue)
        Dim NewSqlBuilder As New MySqlBuilder(My.Settings.MxDatabaseConnectionString, AHash, "tblJobs", "INSERT")
        NewSqlBuilder.RunSQL()
        RaiseEvent NewRecordAddedEvent()

        Me.Close()
    End Sub

    Public Function GetPrimKey()
        Return AcftTailNumberComboBox.SelectedValue
    End Function

End Class

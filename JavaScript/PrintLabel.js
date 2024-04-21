
// Print the address label
async function printLabel(line1, line2, line3, howmanycopies, xmlLabel) {
    try {
        var printerName = dymo.label.framework.getLabelWriterPrinters()[0].name;
    
        const labelXml = xmlLabel.toString();

        var label = dymo.label.framework.openLabelXml(labelXml);

        var line1local = line1.toString();
        var line2local = line2.toString();
        var line3local = line3.toString();

        var label = dymo.label.framework.openLabelXml(labelXml);
        label.setObjectText("Line_1", line1local.toString());
        label.setObjectText("Line_2", line2local.toString());
        label.setObjectText("Line_3", line3local.toString());
       
        var params = dymo.label.framework.createLabelWriterPrintParamsXml({ copies: howmanycopies });                

        // Print the label
        await dymo.label.framework.printLabel(printerName, params, label);
      
        console.log("Address label printed successfully!");
    } catch (error) {
        console.log("Error printing label:", error);
    }
}



async function printBarCodeLabel(line1, barcodenumber, howmanycopies, xmlLabel) {
    try {
    //    alert("GIBS33");
        var printerName = dymo.label.framework.getLabelWriterPrinters()[0].name;

        const labelXml = xmlLabel.toString();

        var label = dymo.label.framework.openLabelXml(labelXml);

        var line1local = line1.toString();
    
        var label = dymo.label.framework.openLabelXml(labelXml);
        label.setObjectText("TextObject0", line1local.toString());
        label.setObjectText("BarcodeObject0", barcodenumber.toString());
       

        var params = dymo.label.framework.createLabelWriterPrintParamsXml({ copies: howmanycopies });

        // Print the label
        await dymo.label.framework.printLabel(printerName, params, label);

        console.log("Address label printed successfully!");
    } catch (error) {
        console.log("Error printing label:", error);
    }
}

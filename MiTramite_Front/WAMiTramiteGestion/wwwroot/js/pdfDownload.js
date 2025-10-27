window.downloadFile = (filename, base64) => {
  const link = document.createElement("a");
  link.href = "data:application/pdf;base64," + base64;
  link.download = filename;
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
};

window.openPdfInNewWindow = (base64) => {
  const newWindow = window.open();
  newWindow.document.write(
    '<iframe width="100%" height="100%" src="data:application/pdf;base64,' +
      base64 +
      '"></iframe>'
  );
};

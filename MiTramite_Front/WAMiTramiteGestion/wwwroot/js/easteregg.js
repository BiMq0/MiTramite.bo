export function iniciarKonami(dotnetHelper) {
  const konamiCode = [
    "ArrowUp",
    "ArrowUp",
    "ArrowDown",
    "ArrowDown",
    "ArrowLeft",
    "ArrowRight",
    "ArrowLeft",
    "ArrowRight",
    "KeyB",
    "KeyA",
  ];

  let inputSequence = [];
  console.log("Konami code listener initialized");
  document.addEventListener("keydown", function (e) {
    inputSequence.push(e.code);
    console.log("Tecla presionada:", e.code);
    if (inputSequence.length > konamiCode.length) {
      inputSequence.shift();
    }
    const currentSequence = inputSequence.join(",");
    const targetSequence = konamiCode.join(",");
    if (currentSequence === targetSequence) {
      inputSequence = [];

      if (
        dotnetHelper &&
        typeof dotnetHelper.invokeMethodAsync === "function"
      ) {
        dotnetHelper
          .invokeMethodAsync("MostrarKratos")
          .then(() => console.log("✅ MostrarKratos llamado exitosamente"))
          .catch((err) => console.error("Error al llamar MostrarKratos:", err));
      } else {
        console.error("dotnetHelper no válido:", dotnetHelper);
      }
    }
  });
}

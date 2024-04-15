// Definição dos pinos
const int buttonPin = 2;    // Pino onde o botão está conectado
const int ledPin = 13;      // Pino do LED

void setup() {
  pinMode(buttonPin, INPUT_PULLUP);  // Configura o pino do botão como entrada com resistor de pull-up interno
  pinMode(ledPin, OUTPUT);           // Configura o pino do LED como saída
  Serial.begin(9600);                // Inicia a comunicação serial a 9600 bps
}

void loop() {
  int buttonState = digitalRead(buttonPin); // Lê o estado do botão

  if (buttonState == LOW) {   // Se o botão for pressionado (aberto)
    digitalWrite(ledPin, LOW); // Desliga o LED
    Serial.println("0");        // Envia "0" para a porta serial indicando botão não pressionado
  } else {
    digitalWrite(ledPin, HIGH);  // Liga o LED se o botão não estiver pressionado
    Serial.println("1");        // Envia "1" para a porta serial indicando botão pressionado
  }

  delay(100); // Delay para estabilizar a leitura e envio de dados
}

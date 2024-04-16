// Definição dos pinos
const int buttonPin1 = 2;   // Pino onde o botão 1 está conectado
const int ledPin1 = 13;     // Pino do LED 1
const int buttonPin2 = 3;   // Pino onde o botão 2 está conectado
const int ledPin2 = 12;     // Pino do LED 2

unsigned long lastButtonPress = 0;   // Armazena o momento do último botão pressionado
const unsigned long delayTime = 5000; // Tempo em milissegundos para apagar os LEDs após ambos os botões estarem desapertados

void setup() {
  pinMode(buttonPin1, INPUT_PULLUP);  // Configura o pino do botão 1 como entrada com resistor de pull-up interno
  pinMode(ledPin1, OUTPUT);           // Configura o pino do LED 1 como saída
  pinMode(buttonPin2, INPUT_PULLUP);  // Configura o pino do botão 2 como entrada com resistor de pull-up interno
  pinMode(ledPin2, OUTPUT);           // Configura o pino do LED 2 como saída
  Serial.begin(9600);                 // Inicia a comunicação serial a 9600 bps
}

void loop() {
  int buttonState1 = digitalRead(buttonPin1); // Lê o estado do botão 1
  int buttonState2 = digitalRead(buttonPin2); // Lê o estado do botão 2

  // Checa se ambos os botões foram pressionados juntos
  if (buttonState1 == LOW && buttonState2 == LOW) {
    digitalWrite(ledPin1, LOW);  // Garante que o LED 1 esteja desligado
    digitalWrite(ledPin2, LOW);  // Garante que o LED 2 esteja desligado
    Serial.println("0");  // Envia "0" para a serial
    lastButtonPress = millis();  // Atualiza o tempo do último botão pressionado
  }
  else if (buttonState1 == LOW) {  // Se somente o botão 1 for pressionado
    digitalWrite(ledPin1, HIGH); // Liga o LED 1
    digitalWrite(ledPin2, LOW);  // Garante que o LED 2 esteja desligado
    Serial.println("1");  // Envia "1" para a serial
    lastButtonPress = millis();  // Atualiza o tempo do último botão pressionado
  } 
  else if (buttonState2 == LOW) { // Se somente o botão 2 for pressionado
    digitalWrite(ledPin1, LOW);  // Garante que o LED 1 esteja desligado
    digitalWrite(ledPin2, HIGH); // Liga o LED 2
    Serial.println("2");  // Envia "2" para a serial
    lastButtonPress = millis();  // Atualiza o tempo do último botão pressionado
  }

  // Desliga os LEDs se ambos os botões estiverem desapertados e o tempo definido passou
  if (buttonState1 == HIGH && buttonState2 == HIGH && millis() - lastButtonPress > delayTime) {
    digitalWrite(ledPin1, LOW);
    digitalWrite(ledPin2, LOW);
    Serial.println("LEDs desligados após timeout"); // Envia uma mensagem quando os LEDs são desligados
  }

  delay(100); // Delay para estabilizar a leitura dos botões
}

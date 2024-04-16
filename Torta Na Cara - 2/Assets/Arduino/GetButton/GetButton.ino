// Definição dos pinos
const int buttonPin1 = 2;   // Pino onde o botão 1 está conectado
const int ledPin1 = 13;     // Pino do LED 1
const int buttonPin2 = 3;   // Pino onde o botão 2 está conectado
const int ledPin2 = 12;     // Pino do LED 2

bool isLocked = false;      // Estado para bloquear a alteração dos LEDs após o primeiro botão ser pressionado

void setup() {
  pinMode(buttonPin1, INPUT_PULLUP);  // Configura o pino do botão 1 como entrada com resistor de pull-up interno
  pinMode(ledPin1, OUTPUT);           // Configura o pino do LED 1 como saída
  pinMode(buttonPin2, INPUT_PULLUP);  // Configura o pino do botão 2 como entrada com resistor de pull-up interno
  pinMode(ledPin2, OUTPUT);           // Configura o pino do LED 2 como saída
  Serial.begin(9600);                // Inicia a comunicação serial a 9600 bps
}

void loop() {
  int buttonState1 = digitalRead(buttonPin1); // Lê o estado do botão 1
  int buttonState2 = digitalRead(buttonPin2); // Lê o estado do botão 2

  if (!isLocked) { // Verifica se os botões ainda não foram bloqueados
    if (buttonState1 == LOW) {  // Se o botão 1 for pressionado
      digitalWrite(ledPin1, HIGH); // Liga o LED 1
      digitalWrite(ledPin2, LOW);  // Garante que o LED 2 esteja desligado
      isLocked = true;             // Bloqueia os botões para evitar mudanças futuras
      Serial.println("Botão 1 pressionado"); // Envia mensagem para a serial
    } 
    else if (buttonState2 == LOW) { // Se o botão 2 for pressionado
      digitalWrite(ledPin2, HIGH); // Liga o LED 2
      digitalWrite(ledPin1, LOW);  // Garante que o LED 1 esteja desligado
      isLocked = true;             // Bloqueia os botões para evitar mudanças futuras
      Serial.println("Botão 2 pressionado"); // Envia mensagem para a serial
    } else {
      Serial.println("Nenhum botão pressionado"); // Envia mensagem quando nenhum botão está pressionado
    }
  }

  delay(100); // Delay para estabilizar a leitura dos botões
}

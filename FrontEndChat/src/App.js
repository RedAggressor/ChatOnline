import { HubConnectionBuilder } from "@microsoft/signalr";
import { WaitingRoom } from "./components/WaitingRoom";
import { useState } from "react";
import { ChatRoom } from "./components/ChatRoom";

function App() {
  const [connection, setConnection] = useState(null);
  const [roomName, setRoomName] = useState("");
  const [messages, setMessages] = useState([]);
  const [userName, setUserName] = useState("");  

  const JoinRoom = async (userName, roomName) => {    

    var connection = new HubConnectionBuilder()
        .withUrl("http://localhost:5143/chatHub")
        .withAutomaticReconnect()
        .build();

        setUserName(userName);

        connection.on("ReceiveMessage", (userName, message, color) => {
          setMessages((messages)=> [...messages, {userName, message, color}]);  
        });

        try{

          await connection.start();
          await connection.invoke("ConnectionRoom", {userName, roomName});

          setConnection(connection);
          setRoomName(roomName);
          setUserName(userName);

        } catch(error){
          console.log(error)
        }
  }

  const sendMessage = async (message) => {

    try{
      await connection.invoke("SendMessage", message);

    }catch(error){
      console.log(error);
      
    }
  }
  
  const closeRoom = async () => {
    await connection.stop();
    setConnection(null);
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      {connection ? ( 
        <ChatRoom 
          messages={messages}
          roomName={roomName} 
          closeChat={closeRoom} 
          sendMessage={sendMessage}
          userName={userName}       
        /> 
      ) : (
        <WaitingRoom JoinRoom={JoinRoom}/> 
      )}        
    </div>
  );
}

export default App;

import { Button, CloseButton, Heading, Input } from "@chakra-ui/react"
import { Message } from "./Message"
import { useEffect, useState, useRef } from "react"

export const ChatRoom = ({messages, roomName, closeChat, sendMessage}) => {

    const [message, setMessage] = useState('');
    const EndMessage = useRef();

    useEffect(()=> {
        EndMessage.current.scrollIntoView();
    }, [messages]);

    const onSendMesage = () => {
        sendMessage(message);
        setMessage('');
    }

    return (
    <div className="w-1/2 bg-white p-8 rounded shadow-lg">
        <div className="flex flex-row justify-between mb-5">
            <Heading size={"lg"}>{roomName}</Heading>
            <CloseButton onClick={() => closeChat()}/>
        </div>
        <div className="flex flex-col overflow-auto scroll-smooth h-96 gap-3 pb-3">
            {messages.map((messageInfo, index) => (
                <Message 
                 messageInfo={messageInfo}
                 key={index}
                />
            ))}
            <span ref={EndMessage}/>
        </div>        
        <div className="flex gap-3">
            <Input 
                type='text' 
                value={message} 
                onChange={(event)=> setMessage(event.target.value)} 
                placeholder="Enter you message"
            />
            <Button colorScheme="blue" onClick={onSendMesage}>Send message</Button>
        </div>
    </div>
    );
}
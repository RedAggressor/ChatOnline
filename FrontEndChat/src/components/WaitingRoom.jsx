import { Heading, Button, Text } from "@chakra-ui/react";
import { useState } from "react";

export const WaitingRoom = ({JoinRoom}) => {

    const [userName, setUserName] = useState();
    const [roomName, setRoomName] = useState();

    const onSubmit = (e) => {
        e.preventDefault();
        JoinRoom(userName, roomName);
    }

    return (
    <form 
      onSubmit={onSubmit} 
      className="max-w-sm w-full bg-white p-8 rounded shadow-lg"
    >
        <Heading>Chat room</Heading>
        <div className="mb-4">
            <Text fontSize={"sm"}>User name</Text>
            <input 
                onChange={(event) => setUserName(event.target.value)}
                name="UserName"
                placeholder="Enter user name"/>
        </div>
        <div className="mb-4">
            <Text fontSize={"sm"}>Channel name</Text>
            <input 
                onChange={(event) => setRoomName(event.target.value)}
                name="ChannelName"
                placeholder="Enter channel name"/>
        </div>
        <Button type="submit"colorScheme="blue">
            Connect to room
        </Button>
    </form>
    );   
}
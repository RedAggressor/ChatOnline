export const Message = ({messageInfo}) => {

const messageStyle = {
    color: messageInfo.color
}
    return (
    <div className="w-fit">
        <span className="text-sm text-slate-600">{messageInfo.userName}</span>
        <div className="p-2 bg-gray-100 rounded-lg shadow-md" style={messageStyle}> 
            {messageInfo.message}
        </div>
    </div>
    );
}
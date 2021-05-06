from dataclasses import dataclass
from dataclasses_json import dataclass_json, LetterCase


@dataclass_json(letter_case=LetterCase.CAMEL)
@dataclass
class Event:
    id: int
    name: str
    type: int
    description: str
    subscribed_count: int
    event_time: str
    duration: float


@dataclass_json(letter_case=LetterCase.CAMEL)
@dataclass
class User:
    id: int
    telegram_id: int
    name: str
    user_name: str
    phone: str

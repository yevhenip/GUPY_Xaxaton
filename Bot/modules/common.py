from aiogram import Dispatcher
from aiogram.dispatcher import FSMContext
from aiogram.types import Message, ReplyKeyboardRemove


async def start(msg: Message, state: FSMContext):
    await state.finish()
    await msg.answer("Привіт! Радий тебе бачити у GUPY.Events! Тут ти можеш організувати або продивитись активні "
                     "івенти. Для початку, обери що ти хочеш: організувати або продивитись активні. Тут все анонімно, "
                     "не переймайся, ти тут як свій)")


async def cancel(msg: Message, state: FSMContext):
    await state.finish()
    await msg.answer("Зрозумів. Відміняю операцію.", reply_markup=ReplyKeyboardRemove())


def register_common_module(dispatcher: Dispatcher):
    dispatcher.register_message_handler(start, commands=["start"], state="*")
    dispatcher.register_message_handler(cancel, commands=["cancel"], state="*")
